using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public abstract class NamedRepository<T>(IDbSession session) : BaseRepository<T>(session) where T : BaseEntity
{
    protected override string Columns => $"{Alias}.name";

    protected override string SearchCondition => $"{Alias}.name LIKE @Search";

    protected override string OrderBy => $"{Alias}.name";

    public Task<T?> GetByName(string name)
    {
        return QuerySingle($"TRIM({Alias}.name) = TRIM(@Name)", new { Name = name });
    }
}
