using CacheServer.Contract.Exceptions;
using CacheServer.Contract.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheServer.Infra.Data
{
    public class CacheProvider
    {        
        private MemoryCache Cache { get; set; }

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

        public Task<CacheItem> GetAsync(object key)
        {
            return Task.Run(() =>
            {
                if (key == null)
                    throw new InvalidCacheKeyException("key not informed!");
                var cacheItem = Cache.Get<CacheItem>(key);
                if (cacheItem == null)
                    throw new CacheKeyNotFoundException($"Item with key {key} not found");
                return Cache.Get<CacheItem>(key);
            });
        }

        public Task<int> CountObjects()
        {
            return Task.Run(() =>
            {
                return Cache.Count;
            });
        }
        
        public Task<CacheItem> AddAsync(CacheItem obj)
        {
            return Task.Run(() =>
            {
                if (!obj.IsValidKey())
                    throw new InvalidCacheKeyException("object to cache is invalid key not informed!");
                var cacheItem = Cache.Get<CacheItem>(obj.Key);
                if (cacheItem != null)
                    throw new DuplicateCacheKeyException($"object with key {obj.Key} already exists!");
                
                
                return Cache.Set<CacheItem>(obj.Key, obj, GetDefaultEntryOptions());
            });
        }
        
        public Task RemoveAsync(object key)
        {
            return Task.Run(() =>
            {
                if (key == null)
                    throw new InvalidCacheKeyException("key not informed!");
                var cacheItem = Cache.Get<CacheItem>(key);
                if (cacheItem == null)
                    throw new KeyNotFoundException($"Item with key {key} not found");
                Cache.Remove(key);
                Cache.Compact(.33);
            });
        }

        public Task<CacheItem> UpdateAsyn(CacheItem obj)
        {
            return Task.Run(() =>
            {
                if (!obj.IsValidKey())
                    throw new InvalidCacheKeyException("key not informed!");
                var cacheItem = Cache.Get<CacheItem>(obj.Key);
                if (cacheItem == null)
                    throw new KeyNotFoundException($"Item with key {obj.Key} not found");
                return Cache.Set<CacheItem>(obj.Key, obj, GetDefaultEntryOptions());
            });
        }

    }
}
