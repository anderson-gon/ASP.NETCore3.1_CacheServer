using CacheServer.Application.Interfaces;
using CacheServer.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
        [Route("CountObjects")]
        public async Task<IActionResult> CountObjects()
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Getting objects on cache");

                int countObjects = await _cacheAppService
                                            .CountObjects()
                                            .ConfigureAwait(false);

                return Response(countObjects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string key)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Getting obj by key : {key}");

                var cacheItem = await _cacheAppService
                                        .GetAsync(key)
                                        .ConfigureAwait(false);

                return Response(cacheItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CacheItem item)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Recording obj by key : {item.Key}");

                var cacheItem = await _cacheAppService
                                        .AddAsync(item)
                                        .ConfigureAwait(false);

                return Response(cacheItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CacheItem item)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Updating obj with key : {item.Key}");

                var cacheItem = await _cacheAppService
                                        .UpdateAsync(item)
                                        .ConfigureAwait(false);

                return Response(cacheItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string key)
        {
            try
            {
                _logger.LogInformation($"{typeof(CacheController)} - Deleting obj with key : {key}");

                

                await _cacheAppService
                        .RemoveAsync(key)
                        .ConfigureAwait(false);


                return Response($"Item with key {key} deleted from cache");
            }
            catch (Exception ex)
            {
                _logger.LogError($"A error ocurr. Message: {ex.Message}");
                return Response(ex, ex.Message);
            }
        }

    }
}
