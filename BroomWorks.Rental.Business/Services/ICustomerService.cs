using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerAsync(Guid customerId);
    Task<Customer> CreateCustomerAsync(string username);
    Task<Customer> GetCustomerByNameAsync(string name);
    Task<Customer[]> GetCustomersAsync();
}
