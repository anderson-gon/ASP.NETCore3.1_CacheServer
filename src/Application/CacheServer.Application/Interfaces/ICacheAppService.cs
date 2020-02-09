
using CacheServer.Contract.Models;

namespace CacheServer.Application.Interfaces
{
    public interface ICacheAppService
    {
        CacheItem Get(object key);
        CacheItem Add(CacheItem obj);
        void Remove(CacheItem obj);

        CacheItem Update(CacheItem obj);
    }
}
