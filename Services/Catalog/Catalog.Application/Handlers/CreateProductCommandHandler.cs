using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Summary = request.Summary,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Brands = request.Brands,
                Types = request.Types
            };

            var product = await _productRepository.CreateProduct(productEntity);

            return _mapper.Map<ProductResponse>(product);
        }
    }
}
