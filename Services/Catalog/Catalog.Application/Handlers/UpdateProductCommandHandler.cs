using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Product = new Product()
            {
                Brands = request.Brands,
                Description = request.Description,
                Id = request.Id,
                ImageFile = request.ImageFile,
                Name = request.Name,
                Price = request.Price,
                Summary = request.Summary,
                Types = request.Types,
            };

            return await _productRepository.UpdateProduct(Product);
        }
    }
}
