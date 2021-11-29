using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.ViewModels;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Extensions
{
    public static class TCODataExtensions
    {
        public static TCOCalculationViewModel CreateTCOViewModel(this TCODataModel data, BundlePriceViewModel price)
            => new TCOCalculationViewModel(data.Volume, (double)price.Price, data.CalculateGoodProducts());

        public static TCOCalculationViewModel CreateTCOViewModel(this TCODataModel data, double price)
            => new TCOCalculationViewModel(data.Volume, price, data.CalculateGoodProducts());

        public static TCOCalculationViewModel CreateTCOViewModel(this TCODataModel data, decimal price)
            => new TCOCalculationViewModel(data.Volume, (double)price, data.CalculateGoodProducts());

        public static double CalculateGoodProducts(this TCODataModel data)
            => data.Primary + data.Secondary;
    }
}
