using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Text;

namespace Performance.String
{
    /* Summary *

        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
          DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2


        | Method              | Mean        | Error       | StdDev      | Median      | Ratio | RatioSD | Gen0     | Gen1   | Allocated  | Alloc Ratio |
        |-------------------- |------------:|------------:|------------:|------------:|------:|--------:|---------:|-------:|-----------:|------------:|
        | PlusEqual           | 52,146.0 ns | 1,758.80 ns | 5,074.54 ns | 50,171.6 ns | 1.009 |    0.13 | 122.6196 | 0.6104 | 1001.93 KB |       1.000 |
        | StringConcat        | 46,059.3 ns |   824.04 ns |   730.49 ns | 46,218.3 ns | 0.891 |    0.08 | 122.6196 | 0.6104 | 1001.93 KB |       1.000 |
        | StringBuilderAppend |  1,293.0 ns |    10.67 ns |     9.98 ns |  1,295.2 ns | 0.025 |    0.00 |   0.5455 | 0.0038 |    4.47 KB |       0.004 |
        | StringJoin          |  5,729.9 ns |    68.97 ns |    61.14 ns |  5,723.6 ns | 0.111 |    0.01 |   1.1978 | 0.0305 |    9.81 KB |       0.010 |
        | SpanStackAlloc      |    394.3 ns |     7.81 ns |     7.31 ns |    395.5 ns | 0.008 |    0.00 |   0.2418 |      - |    1.98 KB |       0.002 |
        | ArrayPoolChar       |    381.2 ns |     5.93 ns |     5.26 ns |    379.1 ns | 0.007 |    0.00 |   0.2418 |      - |    1.98 KB |       0.002 |
    
     For most Scenarios String Builder is the best

        StringBuilder maintains an internal buffer that can append without copying the whole string each time.
        Intuitive.
        In benchmarks, StringBuilder is almost as fast as Span<char>/ArrayPool<char> methods but much easier to maintain.
     */
    [MemoryDiagnoser(true)]
    public class StringConcatenationBenchmark
    {
        private const int Iterations = 1000;

        // ❌ Very inefficient - creates many intermediate strings (O(n^2))
        // ❌ Triggers lots of GC pressure
        // 🔻 Use only for tiny, one-off concatenations (e.g., logging)
        [Benchmark(Baseline = true)]
        public string PlusEqual()
        {
            string result = "";
            for (int i = 0; i < Iterations; i++)
            {
                result += "a";
            }
            return result;
        }

        // ❌ Same problems as += — creates new string each iteration
        // ❌ Slightly better, but still not scalable
        // 🔻 Rarely worth using directly in loops
        [Benchmark]
        public string StringConcat()
        {
            string result = string.Empty;
            for (int i = 0; i < Iterations; i++)
            {
                result = string.Concat(result, "a");
            }
            return result;
        }

        // ✅ Most balanced and recommended for 99% of use cases
        // ✅ Efficient memory usage, amortized allocation
        // ✅ Scales well even for large loops (O(n))
        // ✅ Easy to read and maintain
        [Benchmark]
        public string StringBuilderAppend()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                builder.Append("a");
            }
            return builder.ToString();
        }

        // ✅ Efficient if you already have an array or collection
        // ⚠️ Not good for iterative building (O(n^2) if built inside loop)
        // 🔻 Use when you already have a list of strings
        [Benchmark]
        public string StringJoin()
        {
            var array = new string[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                array[i] = "a";
            }
            return string.Join("", array);
        }

        // ⚡️ Extremely fast — zero heap allocation
        // ⚠️ Only works with small, known-size strings
        // ⚠️ Crashes if size > ~1KB due to stack overflow risk
        // 🔻 Use in performance-critical tight loops for small strings
        [Benchmark]
        public string SpanStackAlloc()
        {
            // WARNING: Stackalloc works only with known, small sizes
            Span<char> buffer = stackalloc char[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                buffer[i] = 'a';
            }
            return new string(buffer);
        }

        // ✅ High performance — minimizes allocations
        // 🔁 Best when reused in performance loops (e.g. logging, text building)
        // ⚠️ Slightly more complex code
        // 🔻 Great for large strings or frequent re-use scenarios
        [Benchmark]
        public string ArrayPoolChar()
        {
            var pool = ArrayPool<char>.Shared;
            char[] buffer = pool.Rent(Iterations);
            try
            {
                for (int i = 0; i < Iterations; i++)
                {
                    buffer[i] = 'a';
                }
                return new string(buffer, 0, Iterations);
            }
            finally
            {
                pool.Return(buffer);
            }
        }
    }
}
