using MatBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class UploadPowerBiFileWithCurrenciesAction
    {
        public UploadPowerBiFileWithCurrenciesAction(IMatFileUploadEntry file,
                                                     string sourceCurrency,
                                                     string destinationCurrency)
        {
            File = file;
            SourceCurrency = sourceCurrency;
            DestinationCurrency = destinationCurrency;
        }

        public IMatFileUploadEntry File { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
    }
}
