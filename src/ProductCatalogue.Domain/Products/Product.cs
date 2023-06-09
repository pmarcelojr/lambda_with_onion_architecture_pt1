
using Ardalis.GuardClauses;
using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Products;

public class Product : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }

    public static Product ListNewProduct(Guid tenantId, string sku, string name, string description, decimal price)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            TenantId = Guard.Against.Default(tenantId, nameof(tenantId)),
            Sku = Guard.Against.NullOrWhiteSpace(sku, nameof(sku)),
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name)),
            Description = Guard.Against.NullOrWhiteSpace(description, nameof(description)),
            Price = new Price(price)
        };
    }

    public void ChangePrice(Price newPrice)
    {
        var previousPrice = Price;
        Price = newPrice;
        QueueEvent(new ProductPriceChangedEvent(this, previousPrice));
    }

    public void UpdateProduct(string name, string description, decimal price)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        Price = new Price(price);
    }
}
