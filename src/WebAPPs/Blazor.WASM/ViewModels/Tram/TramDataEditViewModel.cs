using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.ViewModels.Tram
{
    public class TramDataEditViewModel
    {
        public int? ID { get; set; }
        public DateTime Date { get; set; }

        public ShiftTypes Shift { get; set; }

        public string Dimension { get; set; }

        public int NumberOfLamella { get; set; }

        public int NumberOfTrams { get; set; }
        public string MachineID { get; set; }
    }
}
