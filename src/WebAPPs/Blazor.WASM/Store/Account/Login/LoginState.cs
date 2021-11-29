using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login
{
    public class LoginState
    {
        public LoginState() { }

        public LoginState(string returnURL) : this(default, returnURL, false) { }

        public LoginState(string errorMSG, string returnURL, bool loginSucceeded)
        {
            ErrorMSG = errorMSG;
            ReturnURL = returnURL;
            LoginSucceeded = loginSucceeded;
        }

        public string ErrorMSG { get; private set; }

        public string ReturnURL { get; private set; }

        public bool LoginSucceeded { get; set; }
    }
}
