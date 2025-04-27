using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.Orders.Commands.CreateOrder;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.OrderTests;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Order>> _customerDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateOrderCommandHandler _handler;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IProductRepository> _productsRepositoryMock;

    public CreateOrderCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _customerDbSetMock = new Mock<DbSet<Order>>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _productsRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateOrderCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateOrderSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Order>())
            .Returns(_customerDbSetMock.Object);
        
        var command = new CreateOrderCommand(Guid.NewGuid(), new List<CreateOrderItemCommand>());
        var order = Order.Create(command.CustomerId);
        var product = Product.Create("Product", 20);
        var customer = Customer.Create("John", "Doe", "123 Street", "12345");

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(customer));
        _productsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(product));
        _orderRepositoryMock.Setup(repo => repo.AddAsync(order))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Orders)
            .Returns(_orderRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products)
            .Returns(_productsRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Customers)
            .Returns(_customerRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.SaveChangesAsync())
            .Returns(Task.FromResult(1));
        
        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
        _unitOfWorkMock.Verify(uow => uow.Orders.AddAsync(It.IsAny<Order>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}