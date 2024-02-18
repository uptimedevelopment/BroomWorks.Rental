#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;

namespace BroomWorks.Rental.Business.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerService _customerService;
    private readonly IBroomService _broomService;
    private readonly IApplicationContext _applicationContext;

    public ReservationService(
        IReservationRepository reservationRepository,
        ICustomerService customerService,
        IBroomService broomService,
        IApplicationContext applicationContext)
    {
        _reservationRepository = reservationRepository;
        _customerService = customerService;
        _broomService = broomService;
        _applicationContext = applicationContext;
    }

    public async Task<Reservation> StartReservationAsync(Guid broomId, Guid customerId)
    {
        var broom = await _broomService.GetBroomAsync(broomId);
        var customer = await _customerService.GetCustomerAsync(customerId);

        var reservation = new Reservation
        {
            Broom = broom,
            Customer = customer,
            Start = _applicationContext.GetCurrentTime(),
        };

        _reservationRepository.Add(reservation);
        _reservationRepository.CommitAsync();

        return reservation;
    }

    public async Task EndReservationAsync(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);
        reservation.End = _applicationContext.GetCurrentTime();
        await _reservationRepository.CommitAsync();
    }

    public Task<bool> IsBroomReservedAsync(Guid broomId)
    {
        throw new NotImplementedException();
    }

    public async Task<decimal> GetDiscountForBirthdayAsync(Guid customerId)
    {
        var customer = await _customerService.GetCustomerAsync(customerId);
        var now = _applicationContext.GetCurrentTime();

        if (customer.DateOfBirth.Month == now.Month && customer.DateOfBirth.Date == now.Date)
            return 0.5m;
        else
            return 1;
    }


}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed