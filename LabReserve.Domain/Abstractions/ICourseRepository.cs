﻿using LabReserve.Domain.Entities;

namespace LabReserve.Domain.Abstractions
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<Course> GetByName(string name);
    }
}
