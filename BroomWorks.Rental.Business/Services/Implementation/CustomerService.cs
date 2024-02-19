using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;

namespace BroomWorks.Rental.Business.Services.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> CreateCustomerAsync(string username)
    {
        var customer = new Customer
        {
            Name = username,
            DateOfBirth = DateTimeOffset.UtcNow.Date,
        };

        _customerRepository.Add(customer);
        await _customerRepository.CommitAsync();

        return customer;
    }

    public async Task<Customer> GetCustomerAsync(Guid customerId)
    {
        return await _customerRepository.GetByIdAsync(customerId);
    }

    public async Task<Customer> GetCustomerByNameAsync(string name)
    {
        return await _customerRepository.GetCustomerByNameAsync(name);
    }
}
