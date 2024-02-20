namespace BroomWorks.Rental.Domain.Entities;

public class Broom : Entity
{
    public required string RegistrationNumber { get; set; }
    public bool IsActive { get; set; }
}
