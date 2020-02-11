using CacheServer.Application.Interfaces;
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

        public async Task<CacheItem> AddAsync(CacheItem obj)
        {
            return await _provider.AddAsync(obj);
        }

        public async Task<CacheItem> GetAsync(object key)
        {
            return await _provider.GetAsync(key);
        }

        public async Task RemoveAsync(object key)
        {
            await _provider.RemoveAsync(key);
        }

        public async Task<CacheItem> UpdateAsync(CacheItem obj)
        {
            return await _provider.UpdateAsyn(obj);
        }

        public async Task<int> CountObjects()
        {
            return await _provider.CountObjects();
        }
    }
}
