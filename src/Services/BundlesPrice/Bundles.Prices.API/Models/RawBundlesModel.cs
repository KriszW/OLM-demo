using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Models
{
    public class RawBundlesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string ItemNumber { get; set; }

        [Required]
        public string VendorID { get; set; }

        [Required]
        public string BundleID { get; set; }
    }
}
