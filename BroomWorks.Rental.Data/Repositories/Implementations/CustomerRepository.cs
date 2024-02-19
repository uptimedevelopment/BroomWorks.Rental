using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental.Data.Repositories.Implementations;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<Customer> GetCustomerByNameAsync(string name)
    {
        return await _db.Customers.FirstAsync(x => x.Name == name);
    }
}
