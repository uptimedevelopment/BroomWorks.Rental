
using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services;

public interface IBroomService
{
    Task<Broom> GetBroomAsync(Guid broomId);
    Task DeleteBroomAsync(Guid broomId);
    Task<Broom[]> GetAllBroomsAsync();
}
