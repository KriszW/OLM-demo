using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Models
{
    public class BundleModel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string BundleID { get; set; }
        [Required]
        public double Input { get; set; }

        [Required]
        public double Primary { get; set; }
        [Required]
        public double Secondary { get; set; }

        [Required]
        public double Produced { get; set; }
        [Required]
        public double FS { get; set; }
        [Required]
        public double Waste { get; set; }

        [Required]
        public string Dimension { get; set; }

        [Required]
        public string Quality { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        public string SawmillName { get; set; }

        [Required]
        public string MachineName { get; set; }

        [Required]
        public DateTime FinishedDate { get; set; }

        public double CalculateWastePercentage() => Waste / Input;
    }
}
