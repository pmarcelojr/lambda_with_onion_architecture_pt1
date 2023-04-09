using MediatR;
using ProductCatalogue.Domain.Products;

namespace ProductCatalogue.Application.EventHandlers;

public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
{
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        // Republish to an event bus
        // Do something interesting
        // Send an email promoting the new product?
        // Whatever.
        System.Diagnostics.Debug.WriteLine($"{nameof(ProductPriceChangedEventHandler)} called.");
        return Task.CompletedTask;
    }
}
