using FluentValidation;
using LabReserve.Domain.Abstractions;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.UpdateCourse;

public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IValidator<UpdateCourseCommand> _validator;

    public UpdateCourseHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<UpdateCourseCommand> validator)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _validator = validator;
    }

    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
            throw new ValidationException(validateResult.Errors);

        try
        {
            _unitOfWork.BeginTransaction();
            var course = await _courseRepository.Get(request.CourseId) ?? throw new Exception("Course not found");
            course.Name = request.Name;
            course.UpdatedBy = _authService.Id!.Value;
            await _courseRepository.Update(course);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
