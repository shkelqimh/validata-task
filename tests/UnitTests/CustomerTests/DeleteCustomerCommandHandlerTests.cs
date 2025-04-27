using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.DeleteCustomer;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.CustomerTests;

public class DeleteCustomerCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Customer>> _customerDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteCustomerCommandHandler _handler;

    public DeleteCustomerCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _customerDbSetMock = new Mock<DbSet<Customer>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteCustomerCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteCustomerSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Customer>())
            .Returns(_customerDbSetMock.Object);
        
        var command = new DeleteCustomerCommand(Guid.NewGuid());
        var customer = new Customer();
        
        var customerMock = new Mock<ICustomerRepository>();
        
        customerMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(customer));
        customerMock.Setup(repo => repo.DeleteAsync(customer))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Customers)
            .Returns(customerMock.Object);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.SaveChangesAsync())
            .Returns(Task.FromResult(1));
        
        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
        _unitOfWorkMock.Verify(uow => uow.Customers.DeleteAsync(It.IsAny<Customer>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}