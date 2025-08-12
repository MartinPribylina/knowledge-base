using BenchmarkDotNet.Attributes;

namespace Performance.Lib
{
    /* Summary *

        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
          DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2


        | Method               | Mean      | Error     | StdDev   | Allocated |
        |--------------------- |----------:|----------:|---------:|----------:|
        | ForLoopMathMax       | 536.57 ns |  4.302 ns | 3.814 ns |         - |
        | ForEachMathMax       | 269.38 ns |  3.076 ns | 2.569 ns |         - |
        | ForLoopIf            | 528.25 ns | 10.592 ns | 9.390 ns |         - |
        | ForEachIf            | 267.52 ns |  1.950 ns | 1.824 ns |         - |
        | Linq                 |  43.13 ns |  0.494 ns | 0.438 ns |         - |
        | ForLoopMathMaxRandom | 536.31 ns |  7.741 ns | 8.604 ns |         - |
        | ForEachMathMaxRandom | 270.61 ns |  0.625 ns | 0.554 ns |         - |
        | ForLoopIfRandom      | 522.52 ns |  2.929 ns | 2.597 ns |         - |
        | ForEachIfRandom      | 266.88 ns |  2.033 ns | 1.802 ns |         - |
        | LinqRandom           |  44.87 ns |  0.263 ns | 0.233 ns |         - |
    */

    [MemoryDiagnoser]
    public class MaxBenchmarks
    {
        private int[] data;
        private int[] randomData;

        public MaxBenchmarks()
        {
            data = Enumerable.Range(1, 1000).ToArray();
            Random randNum = new Random();
            randomData = Enumerable.Repeat(randNum.Next(0, 1000), 1000).ToArray();
        }

        [Benchmark]
        public int ForLoopMathMax()
        {
            var max = 0;

            for (int i = 0; i < data.Length; i++)
            {
                max = Math.Max(max, data[i]);
            }

            return max;
        }

        [Benchmark]
        public int ForEachMathMax()
        {
            var max = 0;

            foreach(var num in data)
            {
                max = Math.Max(max, num);
            }

            return max;
        }

        [Benchmark]
        public int ForLoopIf()
        {
            var max = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }

            return max;
        }

        [Benchmark]
        public int ForEachIf()
        {
            var max = 0;

            foreach (var num in data)
            {
                if (num > max)
                {
                    max = num;
                }
            }

            return max;
        }

        [Benchmark]
        public int Linq()
        {
            return data.Max();
        }

        [Benchmark]
        public int ForLoopMathMaxRandom()
        {
            var max = 0;

            for (int i = 0; i < randomData.Length; i++)
            {
                max = Math.Max(max, randomData[i]);
            }

            return max;
        }

        [Benchmark]
        public int ForEachMathMaxRandom()
        {
            var max = 0;

            foreach (var num in randomData)
            {
                max = Math.Max(max, num);
            }

            return max;
        }

        [Benchmark]
        public int ForLoopIfRandom()
        {
            var max = 0;

            for (int i = 0; i < randomData.Length; i++)
            {
                if (randomData[i] > max)
                {
                    max = randomData[i];
                }
            }

            return max;
        }

        [Benchmark]
        public int ForEachIfRandom()
        {
            var max = 0;

            foreach (var num in randomData)
            {
                if (num > max)
                {
                    max = num;
                }
            }

            return max;
        }

        [Benchmark]
        public int LinqRandom()
        {
            return randomData.Max();
        }
    }
}
