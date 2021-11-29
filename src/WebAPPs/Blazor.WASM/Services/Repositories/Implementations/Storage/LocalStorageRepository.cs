using Blazor.Extensions.Storage.Interfaces;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Storage
{
    public class LocalStorageRepository : IStorageRepository
    {
        private ILocalStorage _localStorage;

        public LocalStorageRepository(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> DeleteToken(string name)
        {
            await _localStorage.RemoveItem(name);

            return true;
        }

        public async Task<TData> ReadToken<TData>(string name)
        {
            try
            {
                return await _localStorage.GetItem<TData>(name);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<bool> SaveToken<TData>(string name, TData data)
        {
            await _localStorage.SetItem(name, data);

            return true;
        }
    }
}
