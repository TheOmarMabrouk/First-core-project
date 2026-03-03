using First_core_project.Data;
using First_core_project.Helpers;
using First_core_project.Models;
using First_core_project.Services;
using First_core_project.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

public class ProductServiceTests
{
    private SouqcomContext GetDb()
    {
        var options = new DbContextOptionsBuilder<SouqcomContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new TestSouqcomContext(options);

        context.Products.AddRange(
            new Product { Id = 1, Name = "A", Price = 100, Catid = 1 },
            new Product { Id = 2, Name = "B", Price = 200, Catid = 1 },
            new Product { Id = 3, Name = "C", Price = 50, Catid = 1 }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetProductsByCategory_Should_Paginate()
    {
        var context = GetDb();
        var service = new ProductService(context);

        var param = new PaginationParams
        {
            PageNumber = 1,
            PageSize = 2,
            SortBy = "Price",
            Desc = true
        };

        var result = await service.GetProductsByCategoryAsync(1, param);

        result.Should().HaveCount(2);
        result.First().Price.Should().Be(200);
    }
}