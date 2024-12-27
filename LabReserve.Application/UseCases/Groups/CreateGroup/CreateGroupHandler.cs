using System;
using FluentValidation;
using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IValidator<CreateGroupCommand> _validator;
    public CreateGroupHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork, IAuthService authService, IValidator<CreateGroupCommand> validator)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _validator = validator;
    }

    public async Task Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            _unitOfWork.BeginTransaction();
            var newGroup = new Group
            {
                Name = request.Name,
                IdCourse = request.CourseId,
                CreatedBy = _authService.Id!.Value
            };

            await _groupRepository.Create(newGroup);
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
