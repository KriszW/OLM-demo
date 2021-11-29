using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Tram.SharedAPIModels
{
    public class TramDataViewModel
    {
        public TramDataViewModel() { }

        public int? ID { get; set; }
        public DateTime Date { get; set; }

        public ShiftTypes Shift { get; set; }

        public TramDimensionViewModel Dimension { get; set; }

        public int NumberOfLamella { get; set; }

        public int NumberOfTrams { get; set; }
        public string MachineID { get; set; }
    }
}
