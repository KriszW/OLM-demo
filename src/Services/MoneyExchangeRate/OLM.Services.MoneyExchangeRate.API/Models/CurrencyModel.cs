using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Models
{
    public class CurrencyModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        /// <summary>
        /// ISO-4217 Standard alapján a valuta értéke pl: HUF, EUR, USD
        /// </summary>
        [Required]
        public string ISOCode { get; set; }

        /// <summary>
        /// A valuta átváltási értékek
        /// </summary>
        [Required]
        public List<ExchangeRateModel> Rates { get; set; }
    }
}
