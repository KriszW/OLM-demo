using Microsoft.AspNetCore.Components;
using OLM.Blazor.WASM.Services.Services.Abstractions;
using OLM.Blazor.WASM.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Services.Implementations
{
    public class LivePagesService : ILivePagesService
    {
        private NavigationManager _navManager;
        private LivePagesOptions _livePagesOptions;

        private int index;

        private bool _inLivePage;

        public LivePagesService(NavigationManager navManager, LivePagesOptions livePagesOptions)
        {
            _navManager = navManager;
            _navManager.LocationChanged += _navManager_LocationChanged;
            _livePagesOptions = livePagesOptions;
        }

        private async void _navManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            // Ha true, akkor a user kérvényezte a page váltást
            // Ha false, akkor a NavManager.NavigateTo() függvény váltott paget
            // Tehát ha azt akarom hogy leállítsa a váltogatást ha a user elvált, akkor
            // A true eseménynél kell leállítanom a váltogatást
            if (e.IsNavigationIntercepted == true)
            {
                await Stop();
            }
        }

        public Task Start()
        {
            _inLivePage = true;
            index = 0;

            return Task.Run(async() =>
            {

                while (_inLivePage == true)
                {
                    ManageNavigation();

                    await Task.Delay(_livePagesOptions.LiveWaitingTimeBetweenPages * 1000);
                }
            });
        }

        private void ManageNavigation()
        {
            if (index >= _livePagesOptions.Pages.Count()) index = 0;

            _navManager.NavigateTo(_livePagesOptions.Pages.ElementAt(index));

            index++;
        }

        public Task Stop()
        {
            _inLivePage = false;
            _navManager.LocationChanged -= _navManager_LocationChanged;

            return Task.CompletedTask;
        }
    }
}
