using LabReserve.Domain.Enums;

namespace LabReserve.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Status Status { get; set; }
}