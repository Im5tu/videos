using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Benchmarks.Core;

namespace Linq
{
    public class LinqBenchmarks : DefaultBenchmarkConfig
    {
        private static readonly List<int> _list = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Range(0, 100));
        private static readonly IEnumerable<int> _generic = _list;
        private static readonly ICollection<int> _collection = _list;

        [Benchmark(Baseline = true, Description = "Standard LINQ operation IEnumerable<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Linq_IEnumerable()
        {
            return _generic.Sum();
        }

        [Benchmark(Description = "Standard LINQ operation ICollection<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Linq_ICollection()
        {
            return _collection.Sum();
        }

        [Benchmark(Description = "Standard LINQ operation List<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Linq_List()
        {
            return _list.Sum();
        }

        [Benchmark(Description = "Custom operation IEnumerable<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Custom_IEnumerable()
        {
            var result = 0;

            foreach (var element in _generic)
                result += element;

            return result;
        }

        [Benchmark(Description = "Custom operation ICollection<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Custom_ICollection()
        {
            var result = 0;

            foreach (var element in _collection)
                result += element;

            return result;
        }

        [Benchmark(Description = "Custom operation List<T>"), BenchmarkCategory("LINQ")]
        public int SumOnAllElements_Custom_List()
        {
            var result = 0;

            foreach (var element in _list)
                result += element;

            return result;
        }
    }
}
