using BenchmarkDotNet.Attributes;
using CacheServer.Contract.Models;
using CacheServer.Infra.Data;
using CacheServer.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CacheServer.Benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class CacheCoreBenchmark
    {
        private CacheProvider provider;

        public CacheCoreBenchmark()
        {
            var services = new ServiceCollection();

            ServiceInjectorContainer.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            provider = serviceProvider.GetService<CacheProvider>();
        }

        [Benchmark]
        public async Task<int> CountObjects() => await provider.CountObjects();

        [Benchmark]
        public async Task<CacheItem> AddAsync()
        {
            var item = new CacheItem()
            {
                Key = "1",
                Value = "Anderson"
            };

            return await provider.AddAsync(item);
        }


        [Benchmark]
        public async Task<CacheItem> UpdateAsyn()
        {
            var item = new CacheItem()
            {
                Key = "1",
                Value = "Anderson"
            };

            return await provider.UpdateAsyn(item);
        }

        [Benchmark]
        public async Task<CacheItem> GetAsync() => await provider.GetAsync("1");

        [Benchmark]
        public async Task RemoveAsync()
        {
            await provider.RemoveAsync("1");
        }


        /*
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public CacheCoreBenchmark()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
        */
    }
}
