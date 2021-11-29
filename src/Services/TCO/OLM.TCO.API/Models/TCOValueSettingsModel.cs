using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Models
{
    public class TCOValueSettingsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        [Required]
        public string RawMaterialItemNumber { get; set; }
        [Required]
        public double MaximumDifference { get; set; }
        [Required]
        public double ExpectedTCOValue { get; set; }
    }
}
