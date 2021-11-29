using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine
{
    public class MachineViewModel
    {
        public MachineViewModel() { }
        public LatestBundleViewModel Latest { get; set; }
        public DailyMachineDataViewModel Daily { get; set; }
        public WeeklyMachineDataViewModel Weekly { get; set; }
    }
}
