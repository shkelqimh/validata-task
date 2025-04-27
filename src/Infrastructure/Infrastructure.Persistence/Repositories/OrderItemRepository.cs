using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }
}