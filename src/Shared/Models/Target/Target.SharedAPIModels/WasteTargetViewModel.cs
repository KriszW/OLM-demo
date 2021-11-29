using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Target.SharedAPIModels
{
    public class WasteTargetViewModel
    {
        public WasteTargetViewModel() { }

        public int? ID { get; set; }

        public string Dimension { get; set; }

        public double Target { get; set; }

        public double Intersection { get; set; }
    }
}
