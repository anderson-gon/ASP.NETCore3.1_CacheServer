using CacheServer.Contract.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CacheServer.Infra.Data
{
    public class CacheProvider
    {        
        private MemoryCache Cache { get; set; }

        public CacheProvider()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());            
        }

        public CacheItem Get(object key)
        {
            if (key == null)
                throw new ArgumentException("key not informed!");
            return Cache.Get<CacheItem>(key);
        }
        
        public CacheItem Add(CacheItem obj)
        {
            if (string.IsNullOrEmpty(obj.Key))
                throw new ArgumentException("object to cache is invalid key not informed!");
            return Cache.Set<CacheItem>(obj.Key, obj);
        }
        
        public void Remove(CacheItem obj)
        {
            if (string.IsNullOrEmpty(obj.Key))
                throw new ArgumentException("key not informed!");
            Cache.Remove(obj.Key);
        }

        public CacheItem Update(CacheItem obj)
        {
            if (string.IsNullOrEmpty(obj.Key))
                throw new ArgumentException("key not informed!");
            return Cache.Set<CacheItem>(obj.Key, obj);
        }

    }
}
