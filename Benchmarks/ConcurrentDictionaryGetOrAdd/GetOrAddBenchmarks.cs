using System;
using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using Benchmarks.Core;

namespace ConcurrentDictionaryGetOrAdd
{
    public class GetOrAddBenchmarks : DefaultBenchmarkConfig
    {
        private readonly Guid ExistingKey = Guid.NewGuid();
        private readonly object _newObject = new object();
        private readonly ConcurrentDictionary<Guid, Object> _dictionary = new ConcurrentDictionary<Guid, object>();

        [IterationCleanup]
        public void Cleanup()
        {
            _dictionary.Clear();
        }

        [Benchmark(Baseline = true, Description = "GetOrAdd With Closure"), BenchmarkCategory("New Entry")]
        public object GetOrAdd_WithClosure()
        {
            return _dictionary.GetOrAdd(Guid.NewGuid(), _ => _newObject);
        }

        [Benchmark(Baseline = true, Description = "GetOrAdd With Closure"), BenchmarkCategory("Existing")]
        public object GetOrAdd_WithClosure_ExistingKey()
        {
            return _dictionary.GetOrAdd(ExistingKey, _ => _newObject);
        }

        [Benchmark(Description = "GetOrAdd With Arg"), BenchmarkCategory("New Entry")]
        public object GetOrAdd_WithArg()
        {
            return _dictionary.GetOrAdd(Guid.NewGuid(), (_ , arg) => arg, _newObject);
        }

        [Benchmark(Description = "GetOrAdd With Arg"), BenchmarkCategory("Existing")]
        public object GetOrAdd_WithArg_ExistingKey()
        {
            return _dictionary.GetOrAdd(ExistingKey, (_ , arg) => arg, _newObject);
        }
    }
}
