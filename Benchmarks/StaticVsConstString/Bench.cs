using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Benchmarks.Core;

namespace StaticVsConstString
{
    public class ConstVsStatic_Method_Benchmarks : DefaultBenchmarkConfig
    {
        public const string HelloConstant = nameof(HelloConstant);
        public static readonly string HelloStatic = "HelloStatic";

        [Benchmark(Baseline = true)]
        public string Constant()
        {
            return Implementation(HelloConstant);
        }

        [Benchmark]
        public string Static()
        {
            return Implementation(HelloStatic);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string Implementation(string returnValue) => returnValue;
    }

    public class ConstVsStatic_Dictionary_Benchmarks : DefaultBenchmarkConfig
    {
        private static readonly Dictionary<string, string> _data = new Dictionary<string, string>();
        public const string HelloConstant = nameof(HelloConstant);
        public static readonly string HelloStatic = "HelloStatic";

        [Benchmark(Baseline = true), MethodImpl(MethodImplOptions.NoInlining)]
        public void Constant()
        {
            Check1();
        }

        private void ThrowFromMethod()
        {
            throw new Exception("Testing");
        }

        [Benchmark, MethodImpl(MethodImplOptions.NoInlining)]
        public void Static()
        {
            Check2();
        }

        private void Check1()
        {
            if (_data is null)
                ThrowFromMethod();
        }

        private void Check2()
        {
            if (_data is null)
                throw new Exception("Testing");
        }
    }
}
