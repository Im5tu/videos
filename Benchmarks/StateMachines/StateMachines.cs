using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Benchmarks.Core;

namespace StateMachines
{
    public class StateMachines : DefaultBenchmarkConfig
    {
        [Benchmark(Baseline = true, Description = "async/await")]
        public async Task<string> AsyncAwait()
        {
            return await DoSomethingAsync();
        }

        [Benchmark(Description = "No async/await")]
        public Task<string> Non_AsyncAwait()
        {
            return DoSomethingAsync();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Task<string> DoSomethingAsync()
        {
            return Task.FromResult("Hello World");
        }
    }
}
