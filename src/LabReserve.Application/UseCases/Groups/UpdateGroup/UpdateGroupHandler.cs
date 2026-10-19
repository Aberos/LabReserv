using LabReserve.Domain.Abstractions;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.UpdateGroup;

public class UpdateGroupHandler(IGroupRepository groupRepository, IAuthService authService)
    : ICommandHandler<UpdateGroupCommand>
{
    public async ValueTask<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.Get(request.GroupId) ?? throw new KeyNotFoundException("Group not found");
        group.Name = request.Name;
        group.CourseId = request.CourseId;
        group.UpdatedBy = authService.Id!.Value;

        await groupRepository.Update(group);

        return Unit.Value;
    }
}
