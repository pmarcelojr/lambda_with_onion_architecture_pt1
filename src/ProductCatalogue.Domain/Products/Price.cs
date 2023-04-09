using Ardalis.GuardClauses;
using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Products;

public class Price : ValueObject
{
    public string LanguageCode { get; }
    public decimal Value { get; }

    public Price(decimal price, string langCode = "en")
    {
        Value = Guard.Against.NegativeOrZero(price, nameof(price));
        LanguageCode = Guard.Against.NullOrWhiteSpace(langCode, nameof(langCode));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return LanguageCode;
    }
}
