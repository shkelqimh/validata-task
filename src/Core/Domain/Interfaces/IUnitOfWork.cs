namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IOrderItemRepository OrderItems { get; }

    Task<int> SaveChangesAsync();
}