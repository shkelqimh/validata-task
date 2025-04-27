using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetOrdersByCustomerId(Guid customerId)
    {
       return await _dbSet.Where(order => order.CustomerId == customerId)
            .OrderBy(order => order.CreatedOn)
            .ToListAsync();
    }
}