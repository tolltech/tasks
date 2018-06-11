using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace JsonConversion
{
    class JsonProgram
    {
        static void Main()
        {
            var json = Console.In.ReadToEnd();

            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var before = JsonConvert.DeserializeObject<JsonV2>(json);
            var products = Convert(before.Products);
            using (var writer = new JsonTextWriter(Console.Out))
                serializer.Serialize(writer, new JsonV3 { Version = "3", Products = products });

            Console.ReadKey(true);
        }

        private static IEnumerable<ItemV3> Convert(IDictionary<int, ItemV2> products)
        {
            return
                products.Select(
                    kvp =>
                        new ItemV3 { Id = kvp.Key, Name = kvp.Value.Name, Count = kvp.Value.Count, Price = kvp.Value.Price });
        }
    }

    class ItemV2
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }

    class ItemV3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }

    class JsonV2
    {
        public string Version { get; set; }
        public IDictionary<int, ItemV2> Products { get; set; }
    }

    class JsonV3
    {
        public string Version { get; set; }
        public IEnumerable<ItemV3> Products { get; set; }
    }
}
