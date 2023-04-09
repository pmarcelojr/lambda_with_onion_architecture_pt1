using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Dtos;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = Guard.Against.Null(productsRepository, nameof(productsRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var products = await _productsRepository.GetAllAsync(request.TenantId, cancellationToken);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
