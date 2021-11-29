using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines
{
    public class SummarizedMachineViewModel
    {
        public SummarizedMachineViewModel() { }
        public DailySummarizedViewModel Daily { get; set; }
        public WeeklySummarizedViewModel Weekly { get; set; }
    }
}
