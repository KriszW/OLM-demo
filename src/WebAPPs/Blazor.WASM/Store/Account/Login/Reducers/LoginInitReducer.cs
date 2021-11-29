using Fluxor;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Reducers
{
    public class LoginInitReducer : Reducer<LoginState, LoginInitAction>
    {
        public override LoginState Reduce(LoginState state, LoginInitAction action)
            => new LoginState(action.ReturnURl);
    }
}
