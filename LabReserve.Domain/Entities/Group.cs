namespace LabReserve.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public long IdCourse { get; set; }
}