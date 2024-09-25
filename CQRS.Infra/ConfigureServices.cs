using CQRS.Domain.Repository;
using CQRS.Infra.Data;
using CQRS.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("BlogDbContext") ?? 
                throw new InvalidOperationException("ConnectionString 'BlogDbContext' não encontrada.")));

        services.AddTransient<IBlogRepository, BlogRepository>();
        return services;
    }
}