using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Tram.SharedAPIModels.Response
{
    public class TramResponseViewModel
    {
        public TramResponseViewModel() { }

        public DateTime Date { get; set; }
        public string Dimension { get; set; }
        public int NumberOfLammela { get; set; }
        public int NumberOfTram { get; set; }
    }
}
