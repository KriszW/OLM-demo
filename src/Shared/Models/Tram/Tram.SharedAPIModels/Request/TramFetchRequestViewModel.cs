using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Tram.SharedAPIModels.Request
{
    public class TramFetchRequestViewModel
    {
        public TramFetchRequestViewModel() { }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
