using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Domain.Enums;
using LabReserve.Domain.Helpers;
using MediatR;

namespace LabReserve.Application.UseCases.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();

            var newUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = UserHelper.EncryptPassword(request.Password),
                Status = Status.Enable,
                UserType = request.UserType,
                Phone = request.Phone,
            };

            var newUserId = await _userRepository.Create(newUser);
            if(request.Groups?.Any() ?? false)
            {
                foreach (var group in request.Groups) 
                {
                    await _userRepository.AddGroup(new GroupUser
                    {
                        IdGroup = group,
                        IdUser = newUserId,
                        Status = Status.Enable,
                    });
                }
            }

            _unitOfWork.Commit();
        }
    }
}
