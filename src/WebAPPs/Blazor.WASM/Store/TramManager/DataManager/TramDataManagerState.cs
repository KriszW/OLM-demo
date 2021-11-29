using OLM.Blazor.WASM.ViewModels.Tram;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager
{
    public class TramDataManagerState
    {
        public TramDataManagerState() : this(default, default, default, default) { }

        public TramDataManagerState(string machineID) : this(machineID, default, default, default) { }

        public TramDataManagerState(string machineID,
                                    IEnumerable<string> dimensions,
                                    APIError errors,
                                    TramDataEditViewModel model)
        {
            MachineID = machineID;
            Dimensions = dimensions;
            Errors = errors;
            Model = model;
        }

        public string MachineID { get; set; }

        public IEnumerable<string> Dimensions { get; set; }

        public APIError Errors { get; set; }

        public TramDataEditViewModel Model { get; set; }
    }
}
