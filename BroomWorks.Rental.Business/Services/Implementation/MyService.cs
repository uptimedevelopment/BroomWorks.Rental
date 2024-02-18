namespace BroomWorks.Rental.Business.Services.Implementation;

public class MyService
{
    private readonly IReservationService _reservationService;
    private readonly IBroomService _broomService;

    public MyService(
        IReservationService reservationService,
        IBroomService broomService)
    {
        _reservationService = reservationService;
        _broomService = broomService;
    }

    public async Task DeleteBroomAsync(Guid broomId)
    {
        if (!await _reservationService.IsBroomReservedAsync(broomId))
        {
            await _broomService.DeleteBroomAsync(broomId);
        }
    }
}
