using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Models
{
    public class ProductionTimeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public int WeekNumber { get; set; }

        [Required]
        public string MachineName { get; set; }
    }
}
