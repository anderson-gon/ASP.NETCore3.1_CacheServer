using CacheServer.Application.Interfaces;
using CacheServer.Contract.Exceptions;
using CacheServer.Contract.Models;
using CacheServer.Infra.Data;
using System.Threading.Tasks;

namespace CacheServer.Application.Services
{
    public class CacheAppService : ICacheAppService
    {
        CacheProvider _provider;

        public CacheAppService(CacheProvider provider)
        {
            _provider = provider;
        }

        public async ValueTask<CacheItem> AddAsync(CacheItem obj)
        {
            if (!obj.IsValidKey())
                throw new InvalidCacheKeyException("object to cache is invalid key not informed!");
            var cacheItem = await _provider.GetAsync(obj.Key);
            if (cacheItem != null)
                throw new DuplicateCacheKeyException($"object with key {obj.Key} already exists!");
            return await _provider.AddAsync(obj);
        }

        public async ValueTask<CacheItem> GetAsync(object key)
        {
            _= key ?? throw new InvalidCacheKeyException("key not informed!");
            return await _provider.GetAsync(key) ?? throw new CacheKeyNotFoundException($"Item with key {key} not found");
        }

        public async ValueTask RemoveAsync(object key)
        {
            _ = key ?? throw new InvalidCacheKeyException("key not informed!");
            _ = await _provider.GetAsync(key) ?? throw new CacheKeyNotFoundException($"Item with key {key} not found");
            await _provider.RemoveAsync(key);
        }

        public async ValueTask<CacheItem> UpdateAsync(CacheItem obj)
        {
            if (!obj.IsValidKey())
                throw new InvalidCacheKeyException("object to cache is invalid key not informed!");
            _ = await _provider.GetAsync(obj.Key) ?? throw new CacheKeyNotFoundException($"Item with key {obj.Key} not found");
            return await _provider.UpdateAsyn(obj);
        }

        public async ValueTask<int> CountObjects()
        {
            return await _provider.CountObjects();
        }
    }
}
