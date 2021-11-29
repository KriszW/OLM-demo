using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Models
{
    public class ExchangeRateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        /// <summary>
        /// A valute ISO kódja, amire át szeretnénk váltani
        /// </summary>
        [Required]
        public string DestISOCode { get; set; }

        /// <summary>
        /// A valuta átváltás értéke
        /// </summary>
        [Required]
        public decimal Rate { get; set; }
    }
}
