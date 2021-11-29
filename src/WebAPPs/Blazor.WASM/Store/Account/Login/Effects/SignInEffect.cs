using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Auth.Abstractions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Account;
using OLM.Blazor.WASM.Store.Account.Login.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Account.Login.Effects
{
    public class SignInEffect : Effect<StartSendLoginAction>
    {
        private IIdentityProvider _identityProvider;
        private ILogger<SignInEffect> _logger;

        public SignInEffect(IIdentityProvider identityProvider,
                            ILogger<SignInEffect> logger)
        {
            _identityProvider = identityProvider;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartSendLoginAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _identityProvider.Login(action.Model);

                if (response.Success)
                {
                    dispatcher.Dispatch(new LoginSucceededAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new LoginFailedAction(response.Message));
                }
            }
            catch (Exception ex)
            {
                var msg = $"Bejelentkezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new LoginFailedAction(msg));
            }
        }
    }
}
