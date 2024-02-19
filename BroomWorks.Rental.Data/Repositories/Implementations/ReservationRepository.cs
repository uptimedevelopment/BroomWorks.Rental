using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental.Data.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<Reservation[]> GetReservationsWithBroomsAsync(Guid customerId)
    {
        return await _db.Reservations
            .Where(x => x.Customer.Id == customerId)
            .Include(x => x.Broom)
            .ToArrayAsync();
    }

    public async Task<Broom[]> GetAvailableBroomsAsync()
    {
        return await _db.Brooms
            .Where(x => !_db.Reservations.Any(y => y.Broom.Id == x.Id))
            .ToArrayAsync();
    }

    public async Task<bool> IsBroomReservedAsync(Guid broomId)
    {
        return await _db.Reservations.AnyAsync(x => x.Broom.Id == broomId && x.End == null);
    }
}
