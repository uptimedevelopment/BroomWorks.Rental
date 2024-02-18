using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services;

public interface IReservationService
{
    Task<Reservation> StartReservationAsync(Guid broomId, Guid customerId);
    Task EndReservationAsync(Guid reservationId);

    Task<bool> IsBroomReservedAsync(Guid broomId);
    Task<decimal> GetDiscountForBirthdayAsync(Guid customerId);
}
