using CacheServer.Application.Interfaces;
using CacheServer.Contract.Models;
using CacheServer.Infra.Data;

namespace CacheServer.Application.Services
{
    public class CacheAppService : ICacheAppService
    {
        CacheProvider _provider;

        public CacheAppService(CacheProvider provider)
        {
            _provider = provider;
        }

        public CacheItem Add(CacheItem obj)
        {
            return _provider.Add(obj);
        }

        public CacheItem Get(object key)
        {
            return _provider.Get(key);
        }

        public void Remove(CacheItem obj)
        {
            _provider.Remove(obj);
        }

        public CacheItem Update(CacheItem obj)
        {
            return _provider.Update(obj);
        }
    }
}
