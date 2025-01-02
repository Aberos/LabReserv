using System;
using FluentValidation;
using LabReserve.Domain.Dto;

namespace LabReserve.Application.Extensions;

public static class ValidationExceptionExtensions
{
    public static List<ValidationError> GetValidationErrors(this ValidationException exception)
    {
        var errors = new List<ValidationError>();

        if (exception?.Errors is null)
            return errors;

        foreach (var error in exception.Errors)
        {
            errors.Add(new ValidationError
            {
                PropertyName = error.PropertyName,
                ErrorMessage = error.ErrorMessage
            });
        }

        return errors;
    }
}
