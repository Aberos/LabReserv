using FluentValidation;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Courses.CreateCourse;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IValidator<CreateCourseCommand> _validator;

    public CreateCourseHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<CreateCourseCommand> validator)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _validator = validator;
    }

    public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            _unitOfWork.BeginTransaction();
            var newCourse = new Course
            {
                Name = request.Name,
                CreatedBy = _authService.Id!.Value
            };

            await _courseRepository.Create(newCourse);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
