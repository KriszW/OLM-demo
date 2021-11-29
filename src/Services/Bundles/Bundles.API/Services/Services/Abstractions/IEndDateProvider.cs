using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Abstractions
{
    public interface IEndDateProvider
    {
        DateTime GetEndOfTheDay();
        DateTime GetEndOfTheWeek();
    }
}
