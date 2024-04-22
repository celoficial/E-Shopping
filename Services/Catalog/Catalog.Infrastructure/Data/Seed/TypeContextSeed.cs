using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Seed
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkTypes = typeCollection.Find(p => true).Any();
            string path = Path.Combine("bin", "Debug", "net7.0", "Data", "Seed", "types.json");
            if (!checkTypes)
            {
                var typesData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        typeCollection.InsertOneAsync(type);
                    }
                }
            }
        }
    }
}
