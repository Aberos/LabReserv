using FluentValidation;
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
        private readonly IAuthService _authService;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _validator = validator;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            try
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
                    CreatedBy = _authService.Id!.Value
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
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
