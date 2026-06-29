using BackendAPI.Domain.Entities;
using BackendAPI.Application.Abstractions;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.Repositories;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore_Test.Repository;

public sealed class OrderRepositoryStockTests
{
    [Fact]
    public async Task TryDecreaseStockAsync_Should_Not_Decrease_When_Requested_Quantity_Exceeds_Available_Stock()
    {
        await using var database = await CreateDatabaseAsync();
        var productId = await SeedProductAsync(database.Context, quantity: 5);
        var repository = new OrderRepository(database.Context);

        var firstDecrease = await repository.TryDecreaseStockAsync(productId, quantity: 3, CancellationToken.None);
        var secondDecrease = await repository.TryDecreaseStockAsync(productId, quantity: 3, CancellationToken.None);

        firstDecrease.Should().Be(StockDecreaseResult.Success);
        secondDecrease.Should().Be(StockDecreaseResult.InsufficientStock);

        var product = await database.Context.Product.AsNoTracking().SingleAsync(x => x.ProductId == productId);
        product.Quantity.Should().Be(2);
    }

    [Fact]
    public async Task UnitOfWork_Should_Roll_Back_Stock_Decrease_When_A_Later_Decrease_Fails()
    {
        await using var database = await CreateDatabaseAsync();
        var firstProductId = await SeedProductAsync(database.Context, quantity: 5);
        var secondProductId = await SeedProductAsync(database.Context, quantity: 1);
        var repository = new OrderRepository(database.Context);
        var unitOfWork = new EfUnitOfWork(database.Context);

        var act = async () => await unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var firstDecrease = await repository.TryDecreaseStockAsync(firstProductId, quantity: 3, token);
            firstDecrease.Should().Be(StockDecreaseResult.Success);

            var secondDecrease = await repository.TryDecreaseStockAsync(secondProductId, quantity: 2, token);
            secondDecrease.Should().Be(StockDecreaseResult.InsufficientStock);

            throw new InvalidOperationException("Checkout item stock validation failed.");
        }, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>();

        var products = await database.Context.Product
            .AsNoTracking()
            .Where(x => x.ProductId == firstProductId || x.ProductId == secondProductId)
            .ToDictionaryAsync(x => x.ProductId);

        products[firstProductId].Quantity.Should().Be(5);
        products[secondProductId].Quantity.Should().Be(1);
    }

    private static async Task<TestDatabase> CreateDatabaseAsync()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new UserDbContext(options);
        await context.Database.EnsureCreatedAsync();

        return new TestDatabase(connection, context);
    }

    private static async Task<int> SeedProductAsync(UserDbContext context, int quantity)
    {
        var category = new Category { Name = "Laptop" };
        var screen = new Screen { Size = "14 inch" };
        var ram = new Ram { Capacity = "16GB" };
        var processor = new Processor { CPUTechnology = "Intel" };

        context.Category.Add(category);
        context.Screen.Add(screen);
        context.Ram.Add(ram);
        context.Processor.Add(processor);
        await context.SaveChangesAsync();

        var product = new Product
        {
            NameProduct = $"Product {Guid.NewGuid():N}",
            CategoryId = category.CategoryId,
            ScreenId = screen.ScreenId,
            RamId = ram.RamId,
            ProcessorId = processor.ProcessorId,
            Quantity = quantity,
            Price = 10_000,
            IsDisable = false,
            PublishedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        context.Product.Add(product);
        await context.SaveChangesAsync();

        return product.ProductId;
    }

    private sealed class TestDatabase : IAsyncDisposable
    {
        private readonly SqliteConnection connection;

        public TestDatabase(SqliteConnection connection, UserDbContext context)
        {
            this.connection = connection;
            Context = context;
        }

        public UserDbContext Context { get; }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
            await connection.DisposeAsync();
        }
    }
}
