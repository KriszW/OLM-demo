using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized
{
    public class MachineSummerizedState
    {
        public MachineSummerizedState() : this(true, default) { }

        public MachineSummerizedState(APIResponse<SummarizedMachineViewModel> response) : this(false, response) { }

        public MachineSummerizedState(bool isLoading, APIResponse<SummarizedMachineViewModel> response)
        {
            IsLoading = isLoading;
            Response = response;
        }

        public bool IsLoading { get; private set; }

        public APIResponse<SummarizedMachineViewModel> Response { get; set; }
    }
}
