using OLM.Services.TCO.API.Models;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.ViewModels
{
    public class TCOBundleSummirezed
    {
        public TCOBundleSummirezed(BundlePriceViewModel price)
        {
            Price = price;
            TCOData = new List<TCODataModel>();
        }

        public BundlePriceViewModel Price { get; set; }

        public List<TCODataModel> TCOData { get; set; }
    }
}
