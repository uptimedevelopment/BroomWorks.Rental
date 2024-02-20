using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services;

public interface IReservationService
{
    Task<Reservation> StartReservationAsync(Guid broomId, Guid customerId);
    Task EndReservationAsync(Guid reservationId);

    Task<Reservation[]> GetReservationsForCustomerAsync(Guid customerId);
    Task<Broom[]> GetAvailableBroomsAsync();

    Task<decimal> GetDiscountForBirthdayAsync(Guid customerId);

    Task<Reservation[]> GetReservationsAsync();
    bool IsBroomReserved(Guid id);
    Task DeleteAsync(Guid id);
    Task<decimal> GetDiscountForBirthdayAsync(Customer customer);
    Task<Reservation[]> GetReservationsForCustomerAsync(Customer result);
    Task<Reservation[]> GetReservationsForBroomAsync(Broom broom);
}
