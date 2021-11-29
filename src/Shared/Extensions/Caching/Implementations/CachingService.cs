using Microsoft.Extensions.Caching.Memory;
using OLM.Shared.Extensions.Caching.Abstractions;
using System;
using System.Threading.Tasks;

namespace OLM.Shared.Extensions.Caching.Implementations
{
    public class CachingService<TKey, TObject> : ICachingService<TKey, TObject>
        where TKey : class
        where TObject : class
    {
        private readonly IMemoryCache _cache;

        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public TObject TryGetValueSet(TKey key, Func<TKey, TObject> getter)
        {
            if (_cache.TryGetValue(key, out TObject output) == false)
            {
                output = getter(key);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                };

                _cache.Set(key, output, cacheEntryOptions);
            }

            return output;
        }

        public async Task<TObject> TryGetValueSetAsync(TKey key, Func<TKey, Task<TObject>> getter)
        {
            if (_cache.TryGetValue(key, out TObject output) == false)
            {
                output = await getter(key);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                };

                _cache.Set(key, output, cacheEntryOptions);
            }

            return output;
        }
    }
}
