using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(catalogSpecParams.Search));
                filter &= searchFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, catalogSpecParams.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, catalogSpecParams.TypeId);
                filter &= typeFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                return new Pagination<Product>
                {
                    PageSize = catalogSpecParams.PageSize,
                    PageIndex = catalogSpecParams.PageIndex,
                    Data = await DataFilter(catalogSpecParams, filter),
                    Count = await _context.Products.CountDocumentsAsync(p => true),
                };
            }

            return new Pagination<Product>
            {
                PageSize = catalogSpecParams.PageSize,
                PageIndex = catalogSpecParams.PageIndex,
                Data = await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Ascending("Name"))
                    .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync(),
                Count = await _context.Products.CountDocumentsAsync(p => true),
            };
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, brandName);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByType(string typename)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Types.Name, typename);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    return await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Ascending("Price"))
                    .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync();
                case "priceDesc":
                    return await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Descending("Price"))
                    .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync();
                default:
                    return await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Ascending("Name"))
                    .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync();
            }
        }
    }
}
