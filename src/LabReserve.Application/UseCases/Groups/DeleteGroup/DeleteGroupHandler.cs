using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.DeleteGroup;

public class DeleteGroupHandler(IGroupRepository groupRepository, IAuthService authService)
    : ICommandHandler<DeleteGroupCommand>
{
    public async ValueTask<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.Get(request.GroupId) ?? throw new KeyNotFoundException("Group not found");
        group.UpdatedBy = authService.Id!.Value;

        await groupRepository.Delete(group);

        return Unit.Value;
    }
}
