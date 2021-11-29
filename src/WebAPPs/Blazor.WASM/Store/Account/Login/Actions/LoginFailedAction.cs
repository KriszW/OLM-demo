using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Actions
{
    public class LoginFailedAction
    {
        public LoginFailedAction(string errorMsg)
        {
            ErrorMSG = errorMsg;
        }

        public string ErrorMSG { get; set; }
    }
}
