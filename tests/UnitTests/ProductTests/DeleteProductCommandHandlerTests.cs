using Application.Features.Orders.Commands.DeleteOrder;
using Application.Features.Products.Commands.DeleteProduct;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.ProductTests;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Product>> _productDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteProductCommandHandler _handler;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public DeleteProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _dbContextMock = new Mock<DbContext>();
        _productDbSetMock = new Mock<DbSet<Product>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteProductCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteProductSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Product>())
            .Returns(_productDbSetMock.Object);
        
        var command = new DeleteProductCommand(Guid.NewGuid());
        var product = new Product();
        
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(product));
        _productRepositoryMock.Setup(repo => repo.DeleteAsync(product));
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products)
            .Returns(_productRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.SaveChangesAsync())
            .Returns(Task.FromResult(1));
        
        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
        _unitOfWorkMock.Verify(uow => uow.Products.DeleteAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}