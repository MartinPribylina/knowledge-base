using BenchmarkDotNet.Attributes;
using Iced.Intel;
using MediaBrowser.Model.Text;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Performance.String
{
    /* Summary *

        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
          DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2


        | Method             | Mean     | Error   | StdDev  | Gen0   | Allocated |
        |------------------- |---------:|--------:|--------:|-------:|----------:|
        | Concat             | 126.8 ns | 0.98 ns | 0.87 ns | 0.0238 |     200 B |
        | Interpolation      | 127.7 ns | 0.85 ns | 0.66 ns | 0.0114 |      96 B |
        | Format             | 151.8 ns | 1.89 ns | 1.77 ns | 0.0172 |     144 B |
        | StringBuilder      | 255.2 ns | 4.09 ns | 3.63 ns | 0.0563 |     472 B |
        | StringCreate       | 113.2 ns | 1.68 ns | 1.49 ns | 0.0267 |     224 B |
        | ValueStringBuilder | 132.3 ns | 0.77 ns | 0.68 ns | 0.0153 |     128 B |
    */

    [MemoryDiagnoser]
    public class StringFormattingBenchmark
    {
        private string name = "Martin";
        private int age = 25;
        private double score = 95.5;

        [Benchmark]
        public string Concat() =>
            "Name: " + name + ", Age: " + age + ", Score: " + score;

        [Benchmark]
        public string Interpolation() =>
            $"Name: {name}, Age: {age}, Score: {score}";

        [Benchmark]
        public string Format() =>
            string.Format("Name: {0}, Age: {1}, Score: {2}", name, age, score);

        [Benchmark]
        public string StringBuilder()
        {
            var sb = new StringBuilder();
            sb.Append("Name: ").Append(name)
              .Append(", Age: ").Append(age)
              .Append(", Score: ").Append(score);
            return sb.ToString();
        }

        [Benchmark]
        public string StringCreate() =>
            string.Create(100, (name, age, score), static (span, state) =>
            {
                var (n, a, s) = state;
                var pos = 0;
                "Name: ".AsSpan().CopyTo(span.Slice(pos)); pos += 6;
                n.AsSpan().CopyTo(span.Slice(pos)); pos += n.Length;
                ", Age: ".AsSpan().CopyTo(span.Slice(pos)); pos += 7;
                a.TryFormat(span.Slice(pos), out var written1);
                pos += written1;
                ", Score: ".AsSpan().CopyTo(span.Slice(pos)); pos += 9;
                s.TryFormat(span.Slice(pos), out _); // write double
            });

        [Benchmark]
        public string ValueStringBuilder()
        {
            var vsb = new ValueStringBuilder(stackalloc char[128]);
            vsb.Append("Name: ");
            vsb.Append(name);
            vsb.Append(", Age: ");
            vsb.Append(age.ToString());
            vsb.Append(", Score: ");
            vsb.Append(score.ToString());
            return vsb.ToString();
        }
    }

}
