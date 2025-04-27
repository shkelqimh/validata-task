using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> GetOrdersByCustomerId(Guid customerId);
}