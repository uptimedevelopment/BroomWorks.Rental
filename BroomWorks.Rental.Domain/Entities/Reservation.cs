namespace BroomWorks.Rental.Domain.Entities;

public class Reservation : Entity
{
    public required Customer Customer { get; set; }
    public required Broom Broom { get; set; }

    public DateTimeOffset Start { get; set; }
    public DateTimeOffset? End { get; set; }
    public string Type { get; set; }
}