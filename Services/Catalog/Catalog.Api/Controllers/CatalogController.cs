using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly ISender _sender;

        public CatalogController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery();
            query.Id = id;
            var result = await _sender.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("[action]/{brandName}", Name = "GetAllProductsByBrand")]
        [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProductsByBrand(string brandName)
        {
            var query = new GetProductByBrandQuery();
            query.BrandName = brandName;
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProductsByName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProductsByName()
        {
            var query = new GetProductByNameQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProductsByType")]
        [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProductsByType(string typeName)
        {
            var query = new GetProductByTypeQuery();
            query.TypeName = typeName;
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }

    }
}
