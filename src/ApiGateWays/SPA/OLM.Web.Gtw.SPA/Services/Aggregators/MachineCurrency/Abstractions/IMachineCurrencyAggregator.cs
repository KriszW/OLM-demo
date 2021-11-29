using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.MachineCurrency.Abstractions
{
    public interface IMachineCurrencyAggregator
    {
        Task<MachineViewModel> GetDataForMachine(string machineID, string sourceCurr, string destCurr);
        Task<SummarizedMachineViewModel> GetDataForMachines(string sourceCurr, string destCurr);
    }
}
