using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Actions
{
    public class TokenWriteFailedAction
    {
        public TokenWriteFailedAction(string errorMSG)
        {
            ErrorMSG = errorMSG;
        }

        public string ErrorMSG { get; set; }
    }
}
