using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmarks.Core
{
    [InvocationCount(IterationCount)]
    [MemoryDiagnoser, SimpleJob(RuntimeMoniker.NetCoreApp50), SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [CategoriesColumn, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn, Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public abstract class DefaultBenchmarkConfig
    {
        public const int IterationCount = 1_000_000;
    }
}
