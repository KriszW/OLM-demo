using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace OLM.Services.DailyReport.API.Models
{
    public class DailyReportDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Dimension { get; set; }

        [Required]
        public double Length { get; set; }
        [Required]
        public double LengthOfWaste { get; set; }
        [Required]
        public double LengthOfFS { get; set; }
    }
}
