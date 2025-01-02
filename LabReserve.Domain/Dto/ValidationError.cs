using System;

namespace LabReserve.Domain.Dto;

public class ValidationError
{
    public string PropertyName { get; set; }

    public string ErrorMessage { get; set; }
}
