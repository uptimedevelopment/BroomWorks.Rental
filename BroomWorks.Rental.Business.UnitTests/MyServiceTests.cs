using BroomWorks.Rental.Business.Services.Implementation;
using BroomWorks.Rental.Business.Services;
using BroomWorks.Rental.Domain.Repositories;
using BroomWorks.Rental.Domain;
using NSubstitute;
using Xunit;

namespace BroomWorks.Rental.Business.UnitTests;

public class MyServiceTests
{
    [Fact]
    public async Task DeleteBroom_DeletesBroom_WhenNotReserved()
    {
        // Arrange
        var broomId = Guid.NewGuid();

        var reservationService = Substitute.For<IReservationService>();
        var broomService = Substitute.For<IBroomService>();

        reservationService.IsBroomReservedAsync(broomId).Returns(false);

        var sut = new MyService(reservationService, broomService);

        // Act
        await sut.DeleteBroomAsync(broomId);

        // Assert
        await broomService.Received(1).DeleteBroomAsync(broomId);
    }
}
