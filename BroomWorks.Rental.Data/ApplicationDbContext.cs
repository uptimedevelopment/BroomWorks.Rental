using BroomWorks.Rental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Broom> Brooms => Set<Broom>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {        
    }
}
