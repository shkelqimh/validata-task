using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<bool> ExistsAsync(Guid id);
}