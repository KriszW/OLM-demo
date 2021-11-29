using Fluxor;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Reducers
{
    public class LoginFailedReducer : Reducer<LoginState, LoginFailedAction>
    {
        public override LoginState Reduce(LoginState state, LoginFailedAction action)
            => new LoginState(action.ErrorMSG, state.ReturnURL, false);
    }
}
