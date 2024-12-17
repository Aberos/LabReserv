using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;
using MediatR;

namespace LabReserve.Application.UseCases.Users.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            var user = await _userRepository.Get(request.UserId) ?? throw new Exception("User not found.");
            await _userRepository.RemoveAllGroup(user.Id, _authService.Id!.Value);

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Phone = request.Phone;
            user.UpdatedBy = _authService.Id;
            user.UserType = request.UserType;

            if(request.Groups?.Any() ?? false)
            {
                foreach (var group in request.Groups) 
                {
                    await _userRepository.AddGroup(new GroupUser
                    {
                        IdGroup = group,
                        IdUser = user.Id,
                        Status = Status.Enable,
                        CreatedBy = _authService.Id!.Value
                    });
                }
            }

            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
