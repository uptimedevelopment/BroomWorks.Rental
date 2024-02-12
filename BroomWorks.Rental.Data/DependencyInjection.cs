using BroomWorks.Rental.Data.Repositories.Implementations;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BroomWorks.Rental.Data;

public static class DependencyInjection
{
    public static void RegisterRepositories(this IServiceCollection serviceCollection)
    {
        //serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IBroomRepository, BroomRepository>();
    }
}