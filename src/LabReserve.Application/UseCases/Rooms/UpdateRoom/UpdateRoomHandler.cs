using FluentValidation;
using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.UpdateRoom;

public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IValidator<UpdateRoomCommand> _validator;
    public UpdateRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<UpdateRoomCommand> validator)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _validator = validator;
    }

    public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
            throw new ValidationException(validateResult.Errors);

        try
        {
            _unitOfWork.BeginTransaction();
            var room = await _roomRepository.Get(request.RoomId) ?? throw new Exception("Room not found");
            room.Name = request.Name;
            room.UpdatedBy = _authService.Id!.Value;
            await _roomRepository.Update(room);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
