using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Services.TCO.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Implementations
{
    public class EuroTCOCalculationProvider : ITCOCalculationProviderService
    {
        public double Calculate(TCOCalculationViewModel model)
        {
            return 0.69;
        }
    }
}
