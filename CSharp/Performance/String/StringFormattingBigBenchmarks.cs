using BenchmarkDotNet.Attributes;
using System.Text;

namespace Performance.String
{
    /* Summary *

        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
          DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2


        | Method        | Mean       | Error    | StdDev    |
        |-------------- |-----------:|---------:|----------:|
        | Concat        | 1,180.7 ns | 30.38 ns |  86.69 ns |
        | StringBuilder |   296.5 ns |  5.99 ns |   8.00 ns |
        | StringJoin    |   304.7 ns |  3.35 ns |   2.62 ns |
        | Interpolation |   308.7 ns |  6.16 ns |   7.79 ns |
        | Format        | 3,084.5 ns | 60.57 ns | 112.27 ns |
    */
    public class StringFormattingBigBenchmarks
    {
        private string[] parts;

        public StringFormattingBigBenchmarks()
        {
            // Prepare 50 strings to format
            parts = new string[50];
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = $"Str{i}";
            }
        }

        [Benchmark]
        public string Concat()
        {
            string result = "";
            for (int i = 0; i < parts.Length; i++)
            {
                result += parts[i];
            }
            return result;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                sb.Append(parts[i]);
            }
            return sb.ToString();
        }

        [Benchmark]
        public string StringJoin()
        {
            return string.Join("", parts);
        }

        [Benchmark]
        public string Interpolation()
        {
            // Not very suitable for 50 parts, but let's do one combined string
            return $"{string.Join("", parts)}";
        }

        [Benchmark]
        public string Format()
        {
            // Prepare format string with 50 placeholders: {0}{1}...{49}
            string format = string.Join("", parts.Select((_, i) => $"{{{i}}}"));
            return string.Format(format, parts);
        }
    }
}
