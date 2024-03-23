using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Dto
{
    public class CreateJewelryRequest
    {
        [Required]
        public string SilverJewelryName { get; set; } = null!;
        [Required]
        public string? SilverJewelryDescription { get; set; }
        [Required]
        public decimal? MetalWeight { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public decimal? Price { get; set; }
        [Required]
        [Range(1900, Double.MaxValue)]
        public int? ProductionYear { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;   
        [Required]
        public string? CategoryId { get; set; }
    }
}
