using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Bundle.Prices.APIResponses
{
    public class BundlePriceFileUploadViewModel
    {
        public BundlePriceFileUploadViewModel() { }
        public decimal Rate { get; set; }
        public string Currency { get; set; }
    }
}
