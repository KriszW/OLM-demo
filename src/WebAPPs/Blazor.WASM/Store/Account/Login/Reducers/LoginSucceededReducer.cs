using Fluxor;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Reducers
{
    public class LoginSucceededReducer : Reducer<LoginState, LoginSucceededAction>
    {
        public override LoginState Reduce(LoginState state, LoginSucceededAction action)
            => new LoginState(default, state.ReturnURL, false);
    }
}
