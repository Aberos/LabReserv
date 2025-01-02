namespace LabReserve.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public long CourseId { get; set; }

    public virtual Course Course { get; set; }
}