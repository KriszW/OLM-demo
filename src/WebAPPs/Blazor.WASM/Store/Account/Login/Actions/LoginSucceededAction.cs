using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Actions
{
    public class LoginSucceededAction
    {
        public LoginSucceededAction(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
