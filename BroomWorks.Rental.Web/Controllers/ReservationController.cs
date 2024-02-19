using BroomWorks.Rental.Business.Services;
using BroomWorks.Rental.Web.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BroomWorks.Rental.Web.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IBroomService _broomService;
    private readonly ICustomerService _customerService;

    public ReservationController(
        IReservationService reservationService,
        IBroomService broomService,
        ICustomerService customerService)
    {
        _reservationService = reservationService;
        _broomService = broomService;
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ViewResult> Index()
    {
        var customer = await _customerService.GetCustomerByNameAsync(User.Identity!.Name!);
        var reservations = await _reservationService.GetReservationsForCustomerAsync(customer.Id);
        var brooms = await _reservationService.GetAvailableBroomsAsync();

        var model = new IndexModel
        {
            ActiveReservation = reservations.FirstOrDefault(x => x.End == null),
            AvailableBroom = brooms,
        };

        return View(model);
    }

    [HttpPost("Reservation")]
    public async Task<ViewResult> IndexPost()
    {
        var customer = await _customerService.GetCustomerByNameAsync(User.Identity!.Name!);
        var reservations = await _reservationService.GetReservationsForCustomerAsync(customer.Id);
        var activeReservation = reservations.First(x => x.End == null);
        await _reservationService.EndReservationAsync(activeReservation.Id);

        var brooms = await _reservationService.GetAvailableBroomsAsync();

        var model = new IndexModel
        {
            ActiveReservation = null,
            AvailableBroom = brooms,
        };

        return View("Index", model);
    }

    [HttpGet("Reserve/{id:guid}")]
    public async Task<ViewResult> Reserve(Guid id)
    {
        var broom = await _broomService.GetBroomAsync(id);

        var model = new ReserveModel
        {
            Broom = broom,
            Reservation = null,
        };

        return View(model);
    }

    [HttpPost("Reserve/{id:guid}")]
    public async Task<ViewResult> ReservePost(Guid id)
    {
        var broom = await _broomService.GetBroomAsync(id);

        var model = new ReserveModel
        {
            Broom = broom,
            Reservation = null,
        };

        if (ModelState.IsValid)
        {
            var customer = await _customerService.GetCustomerByNameAsync(User.Identity!.Name!);
            model.Reservation = await _reservationService.StartReservationAsync(id, customer.Id);
        }

        return View("Reserve", model);
    }
}