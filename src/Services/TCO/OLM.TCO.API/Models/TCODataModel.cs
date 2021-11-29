using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Models
{
    public class TCODataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public double Volume { get; set; }

        [Required]
        public double Primary { get; set; }

        [Required]
        public double Secondary { get; set; }

        [Required]
        public string RawMaterialItemNumber { get; set; }
        [Required]
        public string VendorID { get; set; }
        [Required]
        public string BundleID { get; set; }
    }
}
