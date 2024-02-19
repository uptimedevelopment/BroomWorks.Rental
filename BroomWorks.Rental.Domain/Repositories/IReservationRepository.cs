using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Domain.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation[]> GetReservationsWithBroomsAsync(Guid customerId);
    Task<Broom[]> GetAvailableBroomsAsync();
    Task<bool> IsBroomReservedAsync(Guid broomId);
}
