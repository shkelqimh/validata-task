using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.UpdateCustomer;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.CustomerTests;

public class UpdateCustomerCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Customer>> _customerDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _customerDbSetMock = new Mock<DbSet<Customer>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateCustomerCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldUpdateCustomerSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Customer>())
            .Returns(_customerDbSetMock.Object);
        
        var command = new UpdateCustomerCommand(Guid.NewGuid(),"John", "Doe", "123 Street", "12345");
        var customer = Customer.Create(command.FirstName, command.LastName, command.FirstName, command.ZipCode);
        
        var customerMock = new Mock<ICustomerRepository>();
        
        customerMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(customer));
        customerMock.Setup(repo => repo.UpdateAsync(customer))
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
        _unitOfWorkMock.Verify(uow => uow.Customers.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}