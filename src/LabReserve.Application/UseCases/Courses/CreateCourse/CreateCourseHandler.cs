using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Courses.CreateCourse;

public class CreateCourseHandler(ICourseRepository courseRepository, IAuthService authService)
    : ICommandHandler<CreateCourseCommand>
{
    public async ValueTask<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        await courseRepository.Create(new Course
        {
            Name = request.Name,
            CreatedBy = authService.Id!.Value
        });

        return Unit.Value;
    }
}
