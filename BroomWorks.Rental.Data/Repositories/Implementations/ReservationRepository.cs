using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;

namespace BroomWorks.Rental.Data.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext db) : base(db)
    {
    }
}
