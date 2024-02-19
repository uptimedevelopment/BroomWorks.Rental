using BroomWorks.Rental.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Broom> Brooms => Set<Broom>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Broom>().HasData(
        [
            new Broom
            {
                RegistrationNumber = "AAA",
            },
            new Broom
            {
                RegistrationNumber = "BBB",
            },
            new Broom
            {
                RegistrationNumber = "CCC",
            },
            new Broom
            {
                RegistrationNumber = "DDD",
            },
            new Broom
            {
                RegistrationNumber = "EEE",
            },
        ]);
    }
}