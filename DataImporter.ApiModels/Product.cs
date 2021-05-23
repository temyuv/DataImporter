using Newtonsoft.Json;
using System;

namespace DataImporter.ApiModels
{

    [JsonObject(Title = "Product")]
    public class Product
    {

        [JsonProperty("UniqueId")]
        public long UniqueId { get; set; }


        [JsonProperty("Name")]
        public string Name { get; set; }


        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonIgnore]
        public long CompanyId { get; set; }

        [JsonIgnore]
        public long FeedId { get; set; }
    }
}
