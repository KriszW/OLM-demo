using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Auth.Abstractions;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Effects
{
    public class LoginSucceededEffect : Effect<LoginSucceededAction>
    {
        private IAuthenticationService _authService;
        private ILogger<LoginSucceededEffect> _logger;

        public LoginSucceededEffect(IAuthenticationService authService,
                                    ILogger<LoginSucceededEffect> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoginSucceededAction action, IDispatcher dispatcher)
        {
            var msg = "A Token nem lehet null, jelentkezz be újra!";

            if (action.Token != default)
            {
                try
                {
                    await _authService.Login(action.Token);

                    dispatcher.Dispatch(new TokenWriteSucceededAction());
                }
                catch (Exception ex)
                {
                    msg = "A token írása közben váratlan hiba lépett fel, jelentkezz be újra!";
                    _logger.LogError(ex, msg);
                } 
            }

            dispatcher.Dispatch(new TokenWriteFailedAction(msg));
        }
    }
}
