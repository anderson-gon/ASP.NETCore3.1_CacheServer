using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace CacheServer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CacheCoreBenchmark>(
                 DefaultConfig
                    .Instance
                    .With(Job
                            .Default
                            .With(CoreRuntime.Core31)
                            .With(Platform.X64))
                    );
        }
    }
}
