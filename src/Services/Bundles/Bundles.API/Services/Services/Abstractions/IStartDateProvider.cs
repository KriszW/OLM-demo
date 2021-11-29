using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Abstractions
{
    public interface IStartDateProvider
    {
        DateTime GetStartDateForDay();
        DateTime GetStartDateForWeek();
    }
}
