using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Models
{
    public class BundleDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string Dimension { get; set; }

        [Required]
        public string BundleID { get; set; }

        [Required]
        public double AllLength { get; set; }

        [Required]
        public DateTime FinishedDate { get; set; }

        [Required]
        public string MachineName { get; set; }
    }
}
