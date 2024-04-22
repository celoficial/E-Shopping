using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByTypeQueryHandler : IRequestHandler<GetProductByTypeQuery, IList<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByTypeQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IList<Product>> Handle(GetProductByTypeQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _productRepository.GetProductsByType(request.TypeName);
            var productsResponse = _mapper.Map<IList<Product>>(productsList);
            return productsResponse;
        }
    }
}
