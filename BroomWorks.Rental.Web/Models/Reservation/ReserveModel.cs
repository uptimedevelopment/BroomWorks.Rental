using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Web.Models.Reservation;

public class ReserveModel
{
    public required Broom Broom { get; init; }
    public required Domain.Entities.Reservation? Reservation { get; set; }
}