using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Views;

public class GroupUserView
{
    public long IdGroup { get; set; }

    public string NameGroup { get; set; }

    public Status StatusGroup { get; set; }

    public long IdCourse { get; set; }

    public string NameCourse { get; set; }

    public Status StatusCourse { get; set; }

    public long IdUser { get; set; }

    public Status StatusUser { get; set; }

    public Status StatusGroupUser { get; set; }
}