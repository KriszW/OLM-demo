using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Actions
{
    public class StartSendLoginAction
    {
        public StartSendLoginAction(LoginViewModel model)
        {
            Model = model;
        }

        public LoginViewModel Model { get; private set; }
    }
}
