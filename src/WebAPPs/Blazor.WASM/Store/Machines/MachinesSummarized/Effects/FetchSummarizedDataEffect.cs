using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine;
using OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Actions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Effects
{
    public class FetchSummarizedDataEffect : Effect<StartFetchSummarizedDataAction>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly ILogger<FetchSummarizedDataEffect> _logger;

        public FetchSummarizedDataEffect(IMachineRepository machineRepository, ILogger<FetchSummarizedDataEffect> logger)
        {
            _machineRepository = machineRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchSummarizedDataAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _machineRepository.FetchSummarizedData();

                dispatcher.Dispatch(new FetchSummarizedMachineDataWithErrorAction(response));
            }
            catch (Exception ex)
            {
                var msg = $"A gépekhez tartozó adatok lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchSummarizedMachineDataWithErrorAction(new APIResponse<SummarizedMachineViewModel>(msg)));
            }
        }
    }
}
