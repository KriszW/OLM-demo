using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.ViewModels.Settings
{
    public class ServiceUrlSettings
    {
        public ServiceUrlSettings() { }

        public string Identity { get; set; }
        public string Bundles { get; set; }
        public string BundlePrices { get; set; }
        public string MoneyExchangeRate { get; set; }
        public string DailyReport { get; set; }
        public string TCO { get; set; }
        public string Tram { get; set; }
        public string CategoryBulbs { get; set; }
        public string Routing { get; set; }
    }
}
