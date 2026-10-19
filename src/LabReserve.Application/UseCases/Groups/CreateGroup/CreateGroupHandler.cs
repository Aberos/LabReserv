using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.CreateGroup;

public class CreateGroupHandler(IGroupRepository groupRepository, IAuthService authService)
    : ICommandHandler<CreateGroupCommand>
{
    public async ValueTask<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        await groupRepository.Create(new Group
        {
            Name = request.Name,
            CourseId = request.CourseId,
            CreatedBy = authService.Id!.Value
        });

        return Unit.Value;
    }
}
