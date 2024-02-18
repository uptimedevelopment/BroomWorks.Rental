using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;

namespace BroomWorks.Rental.Business.Services.Implementation;

public class BroomService : IBroomService
{
    private readonly IBroomRepository _broomRepository;

    public BroomService(IBroomRepository broomRepository)
    {
        _broomRepository = broomRepository;
    }

    public Task DeleteBroomAsync(Guid broomId)
    {
        throw new NotImplementedException();
    }

    public async Task<Broom> GetBroomAsync(Guid broomId)
    {
        return await _broomRepository.GetByIdAsync(broomId);
    }
}