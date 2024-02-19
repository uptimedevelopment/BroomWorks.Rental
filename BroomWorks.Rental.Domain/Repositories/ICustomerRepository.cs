using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetCustomerByNameAsync(string name);
}
