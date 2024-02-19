using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental.Data.Repositories.Implementations;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _db;

    protected Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _db.Set<T>().FindAsync(id) ?? throw new ArgumentException("Invalid id", nameof(id));
    }

    public void Add(T entity)
    {
        _db.Add(entity);
    }

    public async Task<T[]> GetAllAsync()
    {
        return await _db.Set<T>().ToArrayAsync();
    }
}
