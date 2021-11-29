using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine;
using OLM.Blazor.WASM.Store.Machines.Machine.Actions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.Machine.Effects
{
    public class FetchMachineDataEffect : Effect<StartFetchMachineDataAction>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly ILogger<FetchMachineDataEffect> _logger;

        public FetchMachineDataEffect(IMachineRepository machineRepository,
                                      ILogger<FetchMachineDataEffect> logger)
        {
            _machineRepository = machineRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchMachineDataAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _machineRepository.FetchMachineData(action.MachineID);

                dispatcher.Dispatch(new FetchMachineDataWithErrorAction(response));
            }
            catch (Exception ex)
            {
                var msg = $"'{action.MachineID}' nevű géphez tartozó adatok lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchMachineDataWithErrorAction(new APIResponse<MachineViewModel>(msg)));
            }
        }
    }
}
