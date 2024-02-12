using BroomWorks.Rental.Business.Services;
using BroomWorks.Rental.Business.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace BroomWorks.Rental.Business;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IReservationService, ReservationService>();
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IBroomService, BroomService>();
    }
}
