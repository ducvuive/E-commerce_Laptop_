using BackendAPI.Application.Abstractions;
using BackendAPI.Application.UseCases.Orders.CheckoutOrder;
using BackendAPI.Application.UseCases.Orders.Events;
using BackendAPI.Domain.Entities;
using FluentAssertions;
using Moq;
using ShareView.Constants;
using ShareView.DTO;

namespace LaptopStore_Test.UseCases;

public sealed class CheckoutOrderIdempotencyTests
{
    [Fact]
    public async Task Handle_Should_Return_Existing_Order_And_Not_Decrease_Stock_When_Idempotency_Key_Was_Already_Used()
    {
        var repository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new CheckoutOrderCommandHandler(repository.Object, unitOfWork.Object);
        var command = CreateCommand("duplicate-key");

        repository
            .Setup(x => x.GetCustomerAsync("customer-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CustomerSnapshot("customer-1", "customer@example.com", "customer@example.com"));
        repository
            .Setup(x => x.GetByIdempotencyKeyAsync("customer-1", "duplicate-key", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Invoice
            {
                InvoiceId = 99,
                Status = InvoiceStatus.Pending,
                Total = 2500,
                CustomerId = "customer-1",
                IdempotencyKey = "duplicate-key"
            });
        unitOfWork
            .Setup(x => x.ExecuteInTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
            .Returns<Func<CancellationToken, Task>, CancellationToken>((operation, token) => operation(token));

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.InvoiceId.Should().Be(99);
        result.Value.Total.Should().Be(2500);
        result.Value.Message.Should().Be("Order already exists for this checkout request.");
        repository.Verify(
            x => x.TryDecreaseStockAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never);
        repository.Verify(
            x => x.AddInvoiceAsync(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()),
            Times.Never);
        repository.Verify(
            x => x.AddOrderPlacedEventAsync(It.IsAny<OrderPlacedEvent>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_Save_Idempotency_Key_On_New_Order()
    {
        var repository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new CheckoutOrderCommandHandler(repository.Object, unitOfWork.Object);
        var command = CreateCommand("new-key");
        Invoice? capturedInvoice = null;

        repository
            .Setup(x => x.GetCustomerAsync("customer-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CustomerSnapshot("customer-1", "customer@example.com", "customer@example.com"));
        repository
            .Setup(x => x.GetByIdempotencyKeyAsync("customer-1", "new-key", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Invoice?)null);
        repository
            .Setup(x => x.GetProductsByIdsAsync(It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<int, Product>
            {
                [10] = new()
                {
                    ProductId = 10,
                    NameProduct = "Laptop",
                    Quantity = 5,
                    Price = 2500,
                    IsDisable = false
                }
            });
        repository
            .Setup(x => x.TryDecreaseStockAsync(10, 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(StockDecreaseResult.Success);
        repository
            .Setup(x => x.AddInvoiceAsync(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()))
            .Callback<Invoice, CancellationToken>((invoice, _) =>
            {
                invoice.InvoiceId = 123;
                capturedInvoice = invoice;
            })
            .Returns(Task.CompletedTask);
        repository
            .Setup(x => x.AddOrderPlacedEventAsync(It.IsAny<OrderPlacedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        unitOfWork
            .Setup(x => x.ExecuteInTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
            .Returns<Func<CancellationToken, Task>, CancellationToken>((operation, token) => operation(token));
        unitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.InvoiceId.Should().Be(123);
        capturedInvoice.Should().NotBeNull();
        capturedInvoice!.IdempotencyKey.Should().Be("new-key");
    }

    private static CheckoutOrderCommand CreateCommand(string idempotencyKey)
    {
        return new CheckoutOrderCommand(
            new CheckoutOrderRequestDTO
            {
                IdempotencyKey = idempotencyKey,
                Receiver = "Customer",
                Address = "Address",
                Phone = "0900000000",
                Email = "customer@example.com",
                PaymentMethod = "COD",
                Items = new List<CheckoutOrderItemDTO>
                {
                    new() { ProductId = 10, Quantity = 1 }
                }
            },
            "customer-1");
    }
}
