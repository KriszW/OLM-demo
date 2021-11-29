using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Actions
{
    public class LoginInitAction
    {
        public LoginInitAction(string returnURl)
        {
            ReturnURl = returnURl;
        }

        public string ReturnURl { get; private set; }
    }
}
