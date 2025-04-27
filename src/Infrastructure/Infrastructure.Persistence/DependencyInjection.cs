using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(config => config.UseSqlite(connectionString));
        
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    
    public static void UseDbMigration(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        dbContext!.Database.Migrate();
    }
}