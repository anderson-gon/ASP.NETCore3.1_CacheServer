using CacheServer.Contract.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CacheServer.Infra.Data
{
    public class CacheProvider
    {        
        private MemoryCache Cache { get; set; }
        private object _lockObj = new object();

        private MemoryCacheEntryOptions GetDefaultEntryOptions()
        {
            return new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
        }

        public CacheProvider()
        {
            var cacheOptions = new MemoryCacheOptions();
            cacheOptions.ExpirationScanFrequency = TimeSpan.FromSeconds(60);            

            Cache = new MemoryCache(cacheOptions);            
        }

        public ValueTask<CacheItem> GetAsync(object key)
        {
            lock (_lockObj)
            {
                return new ValueTask<CacheItem>(Cache.Get<CacheItem>(key));
            }
            
        }

        public ValueTask<int> CountObjects()
        {
            lock (_lockObj)
            {
                return new ValueTask<int>(Cache.Count);
            }
        }
        
        public ValueTask<CacheItem> AddAsync(CacheItem obj)
        {
            lock (_lockObj)
            {
                return new ValueTask<CacheItem>(Cache.Set<CacheItem>(obj.Key, obj, GetDefaultEntryOptions()));
            }
        }
        
        public ValueTask RemoveAsync(object key)
        {
            return new ValueTask(Task.Run(() =>
            {
                lock (_lockObj)
                {
                    Cache.Remove(key);
                    Cache.Compact(.33);
                }
            }));
        }

        public ValueTask<CacheItem> UpdateAsyn(CacheItem obj)
        {
            lock (_lockObj)
            {
                return new ValueTask<CacheItem>(Cache.Set<CacheItem>(obj.Key, obj, GetDefaultEntryOptions()));
            }
        }

    }
}
