using BroomWorks.Rental.Business.Services;
using BroomWorks.Rental.Business.Services.Implementation;
using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace BroomWorks.Rental.Business.UnitTests;

public class ReservationServiceTests
{
    [Fact]
    public async Task StartReservation_StartsReservation()
    {
        // Arrange
        var broomId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var reservationRepository = Substitute.For<IReservationRepository>();
        var customerService = Substitute.For<ICustomerService>();
        var broomService = Substitute.For<IBroomService>();
        var applicationContext = Substitute.For<IApplicationContext>();

        var sut = new ReservationService(
            reservationRepository,
            customerService,
            broomService,
            applicationContext);

        // Act
        var reservation = await sut.StartReservationAsync(broomId, customerId);

        // Assert
        reservationRepository.Received(1).Add(reservation);
        await reservationRepository.Received(1).CommitAsync();
    }
}
