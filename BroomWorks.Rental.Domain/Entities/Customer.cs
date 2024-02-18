namespace BroomWorks.Rental.Domain.Entities;

public class Customer : Entity
{
    public required string Name { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
}
