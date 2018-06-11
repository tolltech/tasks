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
            var products = Convert(before.products);
            using (var writer = new JsonTextWriter(Console.Out))
                serializer.Serialize(writer, new JsonV3 { version = "3", products = products });

            Console.ReadKey(true);
        }

        private static IEnumerable<ItemV3> Convert(IDictionary<int, ItemV2> products)
        {
            return
                products.Select(
                    kvp =>
                        new ItemV3 { id = kvp.Key, name = kvp.Value.name, count = kvp.Value.count, price = kvp.Value.price });
        }
    }

    class ItemV2
    {
        public string name { get; set; }
        public double price { get; set; }
        public int count { get; set; }
    }

    class ItemV3
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int count { get; set; }
    }

    class JsonV2
    {
        public string version { get; set; }
        public IDictionary<int, ItemV2> products { get; set; }
    }

    class JsonV3
    {
        public string version { get; set; }
        public IEnumerable<ItemV3> products { get; set; }
    }
}
