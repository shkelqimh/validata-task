using Application.Features.Products.Commands.CreateProduct;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.ProductTests;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Product>> _productDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateProductCommandHandler _handler;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public CreateProductCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _productDbSetMock = new Mock<DbSet<Product>>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateProductCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateProductSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Product>())
            .Returns(_productDbSetMock.Object);
        
        var command = new CreateProductCommand("Product 1", 20);
        var product = Product.Create(command.Name, command.Price);
        
        _productRepositoryMock.Setup(repo => repo.AddAsync(product))
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
        _unitOfWorkMock.Verify(uow => uow.Products.AddAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
