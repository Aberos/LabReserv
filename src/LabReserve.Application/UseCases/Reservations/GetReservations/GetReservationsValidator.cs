using FluentValidation;

namespace LabReserve.Application.UseCases.Reservations.GetReservations;

public class GetReservationsValidator : AbstractValidator<GetReservationsQuery>
{
    private static readonly TimeSpan MaxRange = TimeSpan.FromDays(62);

    public GetReservationsValidator()
    {
        RuleFor(request => request.End)
            .GreaterThan(request => request.Start)
            .OverridePropertyName("end").WithMessage("end must be after start");

        RuleFor(request => request)
            .Must(request => request.End - request.Start <= MaxRange)
            .OverridePropertyName("period").WithMessage("period too long");
    }
}
