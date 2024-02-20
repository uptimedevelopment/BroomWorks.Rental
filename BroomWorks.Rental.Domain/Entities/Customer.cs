namespace BroomWorks.Rental.Domain.Entities;

public class Customer : Entity
{
    public required string Name { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public Guid? ActivatedBy { get; set; }
    public bool IsDangerous { get; set; }
}
