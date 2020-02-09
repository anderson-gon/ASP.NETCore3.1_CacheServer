using CacheServer.Application.Interfaces;
using CacheServer.Application.Services;
using CacheServer.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CacheServer.IoC
{
    public class ServiceInjectorContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Data
            services.AddSingleton<CacheProvider>();

            // Application
            services.AddScoped<ICacheAppService, CacheAppService>();
        }
    }
}
