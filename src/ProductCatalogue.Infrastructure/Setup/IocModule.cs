using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Domain.Repositories;
using ProductCatalogue.Infrastructure.LambdaLogger;

namespace ProductCatalogue.Infrastructure;

public static class IocModule
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IProductsRepository, ProductsRepository>();
        serviceCollection.AddScoped<ILogger, Logger>();
        return serviceCollection;
    }
}
