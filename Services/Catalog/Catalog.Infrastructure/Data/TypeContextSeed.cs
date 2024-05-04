using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(b => true).Any();
        // for testing locally comment next line.
        string path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            // for testing locally uncomment next line.
            //var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");
            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null)
            {
                foreach (var item in types)
                {
                    typeCollection.InsertOneAsync(item);
                }
            }
        }
    }
}