using OLM.Services.TCO.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Services.Abstractions
{
    public interface ITCOCalculationProviderService
    {
        double Calculate(TCOCalculationViewModel model);
    }
}
