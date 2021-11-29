using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Storage
{
    public interface IStorageRepository
    {
        Task<bool> SaveToken<TData>(string name, TData data);

        Task<TData> ReadToken<TData>(string name);

        Task<bool> DeleteToken(string name);
    }
}
