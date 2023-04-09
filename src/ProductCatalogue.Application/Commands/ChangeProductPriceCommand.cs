using Ardalis.GuardClauses;
using MediatR;

namespace ProductCatalogue.Application.Commands;

public class ChangeProductPriceCommand : IRequest<bool>
{
    public ChangeProductPriceCommand()
    {
    }

    public ChangeProductPriceCommand(Guid tenantId, string sku, decimal price)
    {
        TenantId = Guard.Against.Default(tenantId, nameof(tenantId));
        Sku = Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
        NewPrice = Guard.Against.Negative(price, nameof(price));
    }

    public string Sku { get; set; }
    public Guid TenantId { get; set; }
    public decimal NewPrice { get; set; }
}
