using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Models
{
    public class BundleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string Dimension { get; set; }

        [Required]
        public DateTime FinishedDate { get; set; }

        [Required]
        public string MachineName { get; set; }

    }
}
