using BroomWorks.Rental.Business.Services;
using BroomWorks.Rental.Business.Services.Implementation;
using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Domain.Entities;
using BroomWorks.Rental.Domain.Repositories;
using Microsoft.Extensions.Logging;
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
        var logger = Substitute.For<ILogger<ReservationService>>();

        reservationRepository.IsBroomReservedAsync(broomId).Returns(false);

        var sut = new ReservationService(
            reservationRepository,
            customerService,
            broomService,
            applicationContext,
            logger);

        // Act
        var reservation = await sut.StartReservationAsync(broomId, customerId);

        // Assert
        reservationRepository.Received(1).Add(reservation);
        await reservationRepository.Received(1).CommitAsync();
    }

    [Theory]
    //            praegu   soodustus
    [InlineData("1990-02-03", 0.5)]
    [InlineData("2000-02-03", 0.5)]
    public async Task GetDiscountForBirthdayAsync_GivesDiscount(DateTime currentTime, decimal expectedDiscount)
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Tanel",
            DateOfBirth = DateTime.Parse("1990-02-03"),
        };

        var reservationRepository = Substitute.For<IReservationRepository>();
        var customerService = Substitute.For<ICustomerService>();
        var broomService = Substitute.For<IBroomService>();
        var applicationContext = Substitute.For<IApplicationContext>();
        var logger = Substitute.For<ILogger<ReservationService>>();

        customerService.GetCustomerAsync(customer.Id).Returns(customer);
        applicationContext.GetCurrentTime().Returns(currentTime);

        var sut = new ReservationService(
            reservationRepository,
            customerService,
            broomService,
            applicationContext,
            logger);

        // Act
        var actualDiscount = await sut.GetDiscountForBirthdayAsync(customer.Id);

        // Assert
        Assert.Equal(expectedDiscount, actualDiscount);
    }
}
