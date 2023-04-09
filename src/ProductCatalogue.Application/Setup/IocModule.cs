
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Domain.Repositories;
using ProductCatalogue.Infrastructure;

namespace ProductCatalogue.Application.Setup;

public static class IocModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        ConfigureService(serviceCollection);
        return serviceCollection;
    }

    private static void ConfigureService(IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly())
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddPersistenceServices();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
