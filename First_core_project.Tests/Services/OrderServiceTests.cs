using First_core_project.Data;
using First_core_project.Models;
using First_core_project.Services;
using First_core_project.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
public class OrderServiceTests
{
    private SouqcomContext GetDb()
    {
        var options = new DbContextOptionsBuilder<SouqcomContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TestSouqcomContext(options);
    }

    [Fact]
    public async Task Checkout_Should_Fail_When_Cart_Empty()
    {
        var context = GetDb();

        var paymentMock = new Mock<IPaymentService>();
        var loggerMock = new Mock<ILogger<OrderService>>();

        var service = new OrderService(
            context,
            paymentMock.Object,
            loggerMock.Object);

        var result = await service.CheckoutAsync("user1");

        result.Success.Should().BeFalse();
        result.Message.Should().Be("Cart is empty");
    }
}