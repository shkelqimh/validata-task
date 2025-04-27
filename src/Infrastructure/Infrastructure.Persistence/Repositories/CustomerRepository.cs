using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }
}