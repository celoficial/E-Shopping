using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByTypeQuery : IRequest<IList<Product>>
    {
        public string TypeName { get; set; } = string.Empty;
    }
}
