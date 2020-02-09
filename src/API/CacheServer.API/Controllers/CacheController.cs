using CacheServer.Application.Interfaces;
using CacheServer.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace CacheServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ApiController
    {     

        private readonly ILogger<CacheController> _logger;
        private readonly ICacheAppService _cacheAppService;

        public CacheController(ILogger<CacheController> logger,
                               ICacheAppService cacheAppService)
        {
            _logger = logger;
            _cacheAppService = cacheAppService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string key)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Getting obj by key : {key}");

                return Response(_cacheAppService.Get(key));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CacheItem item)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Recording obj by key : {item.Key}");

                return Response(_cacheAppService.Add(item));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] CacheItem item)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Updating obj with key : {item.Key}");

                return Response(_cacheAppService.Update(item));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string key)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Deleting obj with key : {key}");

                var item = _cacheAppService.Get(key);

                if (item == null)
                    throw new InvalidOperationException($"Object with key: {key} not found on the cache!");

                _cacheAppService.Remove(item);


                return Response($"Item with key {item.Key} deleted from cache");
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

    }
}
