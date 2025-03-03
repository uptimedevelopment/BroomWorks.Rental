using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Web.Models.Reservation;

public class IndexModel
{
    public required Domain.Entities.Reservation? ActiveReservation { get; init; }
    public required Broom[] AvailableBrooms { get; init; }
}
