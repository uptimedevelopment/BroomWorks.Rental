#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BroomWorks.Rental.Business.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerService _customerService;
    private readonly IBroomService _broomService;
    private readonly IApplicationContext _applicationContext;
    private readonly ILogger<ReservationService> _logger;

    public ReservationService(
        IReservationRepository reservationRepository,
        ICustomerService customerService,
        IBroomService broomService,
        IApplicationContext applicationContext,
        ILogger<ReservationService> logger)
    {
        _reservationRepository = reservationRepository;
        _customerService = customerService;
        _broomService = broomService;
        _applicationContext = applicationContext;
        _logger = logger;
    }

    public async Task<Reservation> StartReservationAsync(Guid broomId, Guid customerId)
    {
        if (await _reservationRepository.IsBroomReservedAsync(broomId))
        {
            throw new Exception("Broom is already reserved");
        }

        _logger.LogInformation("Starting reservation of broom {broomId} for customer {customerId}", broomId, customerId);

        var broom = await _broomService.GetBroomAsync(broomId);
        var customer = await _customerService.GetCustomerAsync(customerId);

        var reservation = new Reservation
        {
            Broom = broom,
            Customer = customer,
            Start = _applicationContext.GetCurrentTime(),
            End = null,
        };

        _reservationRepository.Add(reservation);
        _reservationRepository.CommitAsync();

        return reservation;
    }

    public async Task EndReservationAsync(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);

        _logger.LogInformation("Ending reservation of broom {broomId} for customer {customerId}", reservation.Broom.Id, reservation.Customer.Id);

        reservation.End = _applicationContext.GetCurrentTime();
        await _reservationRepository.CommitAsync();
    }

    public Task<Reservation[]> GetReservationsForCustomerAsync(Guid customerId)
    {
        return _reservationRepository.GetReservationsWithBroomsAsync(customerId);
    }

    public async Task<Broom[]> GetAvailableBroomsAsync()
    {
        var brooms = await _broomService.GetAllBroomsAsync();

        var availableBrooms = new List<Broom>();
        foreach (var broom in brooms)
        {
            if (await _reservationRepository.IsBroomReservedAsync(broom.Id) == false)
            {
                availableBrooms.Add(broom);
            }
        }
        return [.. availableBrooms];
    }

    public async Task<decimal> GetDiscountForBirthdayAsync(Guid customerId)
    {
        var customer = await _customerService.GetCustomerAsync(customerId);
        var now = _applicationContext.GetCurrentTime();

        if (customer.DateOfBirth.Month == now.Month && customer.DateOfBirth.Day == now.Day)
            return 0.5m;
        else
            return 1;
    }
}
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed