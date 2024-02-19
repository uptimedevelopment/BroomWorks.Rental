using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services;

public interface IReservationService
{
    Task<Reservation> StartReservationAsync(Guid broomId, Guid customerId);
    Task EndReservationAsync(Guid reservationId);

    Task<Reservation[]> GetReservationsForCustomerAsync(Guid customerId);
    Task<Broom[]> GetAvailableBroomsAsync();

    Task<decimal> GetDiscountForBirthdayAsync(Guid customerId);
}
