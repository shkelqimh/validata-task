using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
    public IOrderItemRepository OrderItems { get; }
    
    public UnitOfWork(
        AppDbContext context,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IOrderItemRepository orderItemRepository)
    {
        _context = context;
        Customers = customerRepository;
        Orders = orderRepository;
        Products = productRepository;
        OrderItems = orderItemRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}