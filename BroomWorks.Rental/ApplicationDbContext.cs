using Microsoft.EntityFrameworkCore;

namespace BroomWorks.Rental;
internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
