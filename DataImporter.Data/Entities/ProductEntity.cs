using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DataImporter.Data.Entities
{

    [Table("Product")]
    public class ProductEntity
    {
        [Required]
        public long UniqueId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Description { get; set; }

        [Required]
        public long CompanyId { get; set; }

        public CompanyEntity Company { get; set; }

        [Required]
        public long FeedId { get; set; }

        public FeedEntity Feed { get; set; }
    }
}
