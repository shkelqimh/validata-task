using Application.Features.Products.Commands.UpdateProduct;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.ProductTests;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Product>> _productDbSetMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _productDbSetMock = new Mock<DbSet<Product>>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateProductCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldUpdateProductSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Product>())
            .Returns(_productDbSetMock.Object);
        
        var command = new UpdateProductCommand(Guid.NewGuid(), "Product 1", 20);
        var product = new Product();
        product.Update(command.Name, command.Price);
                
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(product));
        _productRepositoryMock.Setup(repo => repo.UpdateAsync(product))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products)
            .Returns(_productRepositoryMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.SaveChangesAsync())
            .Returns(Task.FromResult(1));
        
        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
        _unitOfWorkMock.Verify(uow => uow.Products.UpdateAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}