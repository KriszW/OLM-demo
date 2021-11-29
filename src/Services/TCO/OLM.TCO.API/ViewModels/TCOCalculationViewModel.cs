using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.ViewModels
{
    public class TCOCalculationViewModel
    {
        public TCOCalculationViewModel(double allVolume, double price, double goodProducts)
        {
            AllVolume = allVolume;
            Price = price;
            GoodProducts = goodProducts;
        }

        /// <summary>
        /// Köbméterben az összes fa mennyiség
        /// </summary>
        public double AllVolume { get; set; }
        /// <summary>
        /// A megadott fának az ára
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// A "jó" faanyag méterben
        /// </summary>
        public double GoodProducts { get; set; }
    }
}
