using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public ProductBrand Brands { get; set; } = null!;
        public ProductType Types { get; set; } = null!;
    }
}
