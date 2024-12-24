using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Rooms.DeleteRoom;

public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public DeleteRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var room = await _roomRepository.Get(request.RoomId) ?? throw new Exception("Room not found");
            room.UpdatedBy = _authService.Id!.Value;
            await _roomRepository.Delete(room);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
