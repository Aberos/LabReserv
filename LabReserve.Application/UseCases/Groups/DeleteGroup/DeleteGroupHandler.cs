using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.DeleteGroup;

public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    public DeleteGroupHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            var group = await _groupRepository.Get(request.GroupId) ?? throw new Exception("Group not found");
            group.UpdatedBy = _authService.Id!.Value;
            await _groupRepository.Delete(group);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
