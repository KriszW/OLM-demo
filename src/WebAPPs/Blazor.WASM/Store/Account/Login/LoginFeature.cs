using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login
{
    public class LoginFeature : Feature<LoginState>
    {
        public override string GetName() => "Signin In";

        protected override LoginState GetInitialState()
            => new LoginState();
    }
}
