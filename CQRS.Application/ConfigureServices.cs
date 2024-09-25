using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CQRS.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(ctg =>
        {
            ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}