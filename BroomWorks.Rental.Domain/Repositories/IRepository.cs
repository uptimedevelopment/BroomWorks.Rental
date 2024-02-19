using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task CommitAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T[]> GetAllAsync();
    void Add(T entity);
}
