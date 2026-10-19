using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;

namespace LabReserve.Persistence.Repositories;

public class CourseRepository(IDbSession session) : NamedRepository<Course>(session), ICourseRepository
{
    protected override string Table => "courses";

    public Task<long> Create(Course entity)
    {
        return Insert("name", "@Name", entity);
    }

    public Task Update(Course entity)
    {
        return UpdateColumns("name = @Name", entity);
    }
}
