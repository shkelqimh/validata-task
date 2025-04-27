using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await GetByIdAsync(id) is not null;
    }
}