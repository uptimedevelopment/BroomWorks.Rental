using BroomWorks.Rental.Domain;
using System.Security.Claims;

namespace BroomWorks.Rental.Web.Services;

public class ApplicationContext : IApplicationContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? CustomerId()
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var customerIdValue = _httpContextAccessor.HttpContext.User.FindFirstValue("customerid");
            if (customerIdValue != null && Guid.TryParse(customerIdValue, out Guid customerId))
            {
                return customerId;
            }
        }

        return null;
    }

    public DateTimeOffset GetCurrentTime()
    {
        return DateTimeOffset.UtcNow;
    }
}
