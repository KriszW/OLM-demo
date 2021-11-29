using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OLM.Shared.Models.Tram.SharedAPIModels;

namespace OLM.Services.Tram.API.Models
{
    public class TramDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ShiftTypes Shift { get; set; }

        [Required]
        public TramDimensionModel Dimension { get; set; }

        [Required]
        public int NumberOfLamella { get; set; }

        [Required]
        public int NumberOfTrams { get; set; }
        [Required]
        public string MachineID { get; set; }
    }
}
