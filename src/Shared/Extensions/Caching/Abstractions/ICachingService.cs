using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Extensions.Caching.Abstractions
{
    public interface ICachingService<TKey, TObject>
        where TKey : class
        where TObject : class
    {
        TObject TryGetValueSet(TKey key, Func<TKey, TObject> getter);
        Task<TObject> TryGetValueSetAsync(TKey key, Func<TKey, Task<TObject>> getter);
    }
}
