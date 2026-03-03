using First_core_project.Data;
using First_core_project.Models;
using First_core_project.Services;
using First_core_project.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class HomeServiceTests
{
    private SouqcomContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<SouqcomContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new TestSouqcomContext(options);

        context.Categories.Add(new Category { Id = 1, Name = "TestCat" });
        context.Products.Add(new Product { Id = 1, Name = "TestProduct" });
        context.Reviews.Add(new Review { Id = 1, Name = "User1" });

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetHomeDataAsync_Should_Return_Data()
    {
        var context = GetDbContext();
        var service = new HomeService(context);

        var result = await service.GetHomeDataAsync();

        result.Should().NotBeNull();
        result.Categories.Should().HaveCount(1);
        result.Products.Should().HaveCount(1);
        result.Reviews.Should().HaveCount(1);
    }
}