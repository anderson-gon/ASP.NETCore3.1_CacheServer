
using CacheServer.Contract.Models;
using System.Threading.Tasks;

namespace CacheServer.Application.Interfaces
{
    public interface ICacheAppService
    {
        Task<CacheItem> GetAsync(object key);
        Task<CacheItem> AddAsync(CacheItem obj);
        Task RemoveAsync(object key);

        Task<CacheItem> UpdateAsync(CacheItem obj);
        Task<int> CountObjects();
    }
}
