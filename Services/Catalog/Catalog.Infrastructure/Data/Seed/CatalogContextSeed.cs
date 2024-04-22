using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Seed
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(p => true).Any();
            string path = Path.Combine("bin", "Debug", "net7.0", "Data", "Seed", "products.json");
            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        productCollection.InsertOneAsync(product);
                    }
                }
            }
        }
    }
}
