using Application.Features.Customers.Commands.CreateCustomer;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.CustomerTests;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<DbContext> _dbContextMock;
    private readonly Mock<DbSet<Customer>> _customerDbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _dbContextMock = new Mock<DbContext>();
        _customerDbSetMock = new Mock<DbSet<Customer>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateCustomerCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCustomerSuccessfully()
    {
        // arrange
        _dbContextMock.Setup(db => db.Set<Customer>())
            .Returns(_customerDbSetMock.Object);
        
        var command = new CreateCustomerCommand("John", "Doe", "123 Street", "12345");
        var customer = Customer.Create(command.FirstName, command.LastName, command.FirstName, command.ZipCode);
        
        var customerMock = new Mock<ICustomerRepository>();
        
        customerMock.Setup(repo => repo.AddAsync(customer))
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
        _unitOfWorkMock.Verify(uow => uow.Customers.AddAsync(It.IsAny<Customer>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
