using Application.Features.Orders.Commands.DeleteOrder;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.OrderTests;

public class DeleteOrderCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Order>> _orderDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteOrderCommandHandler _handler;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    public DeleteOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _dbContextMock = new Mock<DbContext>();
        _orderDbSetMock = new Mock<DbSet<Order>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteOrderCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteOrderSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Order>())
            .Returns(_orderDbSetMock.Object);
        
        var command = new DeleteOrderCommand(Guid.NewGuid());
        var order = new Order();
        
        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(order));
        _orderRepositoryMock.Setup(repo => repo.DeleteAsync(order));
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Orders)
            .Returns(_orderRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.SaveChangesAsync())
            .Returns(Task.FromResult(1));
        
        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
        _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(It.IsAny<Order>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}