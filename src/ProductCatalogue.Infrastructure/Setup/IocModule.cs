using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Infrastructure;

public static class IocModule
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IProductsRepository, ProductsRepository>();
        return serviceCollection;
    }
}
