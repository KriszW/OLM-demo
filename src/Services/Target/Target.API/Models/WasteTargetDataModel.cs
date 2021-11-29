using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Models
{
    public class WasteTargetDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string Dimension { get; set; }

        [Required]
        public double Target { get; set; }

        [Required]
        public double Intersection { get; set; }
    }
}
