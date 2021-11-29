using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Models
{
    public class BundlePriceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string RawMaterialItemNumber { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string VendorID { get; set; }

        [Required]
        public string Currency { get; set; }

        public override string ToString()
        {
            return $"{ID};{RawMaterialItemNumber};{VendorID};{Price};{Currency}";
        }
    }
}
