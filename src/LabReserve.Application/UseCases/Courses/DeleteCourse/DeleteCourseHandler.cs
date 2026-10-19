using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.DeleteCourse;

public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    public DeleteCourseHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var course = await _courseRepository.Get(request.CourseId) ?? throw new Exception("Course not found");
            course.UpdatedBy = _authService.Id!.Value;
            await _courseRepository.Delete(course);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
