using FluentValidation;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.CreateRoom;

public class CreateRoomHandler : IRequestHandler<CreateRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IValidator<CreateRoomCommand> _validator;

    public CreateRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<CreateRoomCommand> validator)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _validator = validator;
    }

    public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            _unitOfWork.BeginTransaction();
            var newRoom = new Room
            {
                Name = request.Name,
                CreatedBy = _authService.Id!.Value
            };

            await _roomRepository.Create(newRoom);
            _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
