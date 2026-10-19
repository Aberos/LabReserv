namespace LabReserve.Domain.Views;

public class ReservationView
{
    public long IdReserve { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public long IdRoom { get; set; }

    public string NameRoom { get; set; } = string.Empty;

    public long IdUser { get; set; }

    public string FirstNameUser { get; set; } = string.Empty;

    public string LastNameUser { get; set; } = string.Empty;

    public long IdGroup { get; set; }

    public string NameGroup { get; set; } = string.Empty;

    public string NameCourse { get; set; } = string.Empty;
}
