using Application.Features.OrderItems.Commands.UpdateOrderItem;
using Application.Features.Orders.Commands.UpdateOrder;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.OrderTests;

public class UpdateOrderCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Order>> _customerDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateOrderCommandHandler _handler;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IProductRepository> _productsRepositoryMock;

    public UpdateOrderCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _customerDbSetMock = new Mock<DbSet<Order>>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _productsRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateOrderCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateOrderSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Order>())
            .Returns(_customerDbSetMock.Object);
        
        var command = new UpdateOrderCommand(Guid.NewGuid(),Guid.NewGuid(), "Pending",new List<UpdateOrderItemCommand>());
        var order = Order.Create(command.Id);
        
        _orderRepositoryMock.Setup(repo => repo.UpdateAsync(order))
            .Returns(Task.CompletedTask);
        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), order => order.OrderItems))
            .Returns(Task.FromResult(order));
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
        _unitOfWorkMock.Verify(uow => uow.Orders.UpdateAsync(It.IsAny<Order>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}