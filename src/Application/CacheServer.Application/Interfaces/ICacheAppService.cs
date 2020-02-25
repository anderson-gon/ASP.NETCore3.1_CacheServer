
using CacheServer.Contract.Models;
using System.Threading.Tasks;

namespace CacheServer.Application.Interfaces
{
    public interface ICacheAppService
    {
        ValueTask<CacheItem> GetAsync(object key);
        ValueTask<CacheItem> AddAsync(CacheItem obj);
        ValueTask RemoveAsync(object key);

        ValueTask<CacheItem> UpdateAsync(CacheItem obj);
        ValueTask<int> CountObjects();
    }
}
