using BroomWorks.Rental.Domain.Entities;

namespace BroomWorks.Rental.Business.Services.Implementation;

public interface IArchiveService
{
    Task MarkBroomAsDeactivatedAsync(Broom broom);

    void MarkBroomAsDeactivated(Broom broom);
}