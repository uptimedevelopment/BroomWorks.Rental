namespace BroomWorks.Rental.Domain;

public interface IApplicationContext
{
    DateTimeOffset GetCurrentTime();
    Guid? CustomerId();
}
