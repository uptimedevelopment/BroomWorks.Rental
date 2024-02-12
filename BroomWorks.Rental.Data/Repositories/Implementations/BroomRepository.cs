using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;

namespace BroomWorks.Rental.Data.Repositories.Implementations;

public class BroomRepository : Repository<Broom>, IBroomRepository
{
    public BroomRepository(ApplicationDbContext db) : base(db)
    {
    }
}
