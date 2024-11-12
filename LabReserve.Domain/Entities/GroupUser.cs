using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Entities;

public class GroupUser
{
    public long IdGroup { get; set; }

    public long IdUser { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Status Status { get; set; }
}