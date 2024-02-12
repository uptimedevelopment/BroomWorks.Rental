using BroomWorks.Rental.Domain;

namespace BroomWorks.Rental.Web.Services;

public class ApplicationContext : IApplicationContext
{
    private static readonly Guid _system = Guid.Parse("58d6b1a1-2fff-4d9a-8014-8ca0d75dd48e");

    public Guid CustomerId()
    {
        return _system;
    }

    public DateTimeOffset GetCurrentTime()
    {
        return DateTimeOffset.UtcNow;
    }
}
