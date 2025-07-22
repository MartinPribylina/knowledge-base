using BenchmarkDotNet.Attributes;

namespace Performance.Iterations
{
    /* Summary *

        BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6093/22H2/2022Update)
        AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
        .NET SDK 9.0.102
          [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
          DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2


        | Method             | Mean        | Error     | StdDev    | Gen0   | Allocated |
        |------------------- |------------:|----------:|----------:|-------:|----------:|
        | ForLoop            |   543.40 ns | 10.465 ns | 12.051 ns |      - |         - |
        | ForeachLoop        |   295.82 ns |  2.709 ns |  2.534 ns |      - |         - |
        | SpanLoop           |   293.89 ns |  2.935 ns |  2.602 ns |      - |         - |
        | LinqSum            |    79.72 ns |  0.587 ns |  0.520 ns |      - |         - |
        | ForeachLoopEven    |   582.48 ns | 11.285 ns | 10.004 ns |      - |         - |
        | LinqSumEven        |   972.12 ns | 14.925 ns | 13.961 ns | 0.0057 |      48 B |
        | ForeachLoopSquared |   270.10 ns |  2.078 ns |  1.944 ns |      - |         - |
        | LinqSumSquared     | 1,203.03 ns | 19.638 ns | 17.409 ns | 0.0057 |      48 B |
        
        LINQ Sum is greatly optimized, but when using Select or Where it slows down.
        LINQ is best for clarity and maintainability when performance is not critical or data sets are smaller.

        I was wondering about Entity Framework since that use linq as well.
            When you use LINQ with EF (e.g. db.Users.Where(...)), you're not actually executing code over .NET collections 
            — you're building an expression tree that EF will translate into SQL and send to the database.

            var activeUsers = await db.Users
                .Where(u => u.IsActive)
                .ToListAsync();

            Gets translated to:

            SELECT * FROM Users WHERE IsActive = 1

            When LINQ is not okay in EF (This would be slower then foreach)
            
            var users = await db.Users.ToListAsync();
            var active = users.Where(u => u.IsActive); // This is LINQ over in-memory data

        Performance tips for LINQ in EF
            Do This	                                                               Not This
            Use .Where() and .Select() before .ToList()	                           Don’t use LINQ after calling .ToList()
            Use .AnyAsync(), .CountAsync()	                                       Avoid loading just to count/filter
            Project only needed fields: .Select(u => new { u.Id, u.Name })	       Don’t fetch whole entity if not needed
            Use .AsNoTracking() for read-only queries	                           Don’t track everything by default
            Avoid .Include() chains unless necessary	                           Lazy loading can create N+1 issues

        AsNoTracking()
            By default, EF Core tracks every entity it loads from the database. This means:

            EF keeps an internal snapshot of the entity's original state.

            If you change a property, EF knows it was modified.

            On SaveChanges(), EF only updates changed entities.

            // Tracked
            var user = await db.Users.FirstAsync();
            user.Name = "Martin";
            await db.SaveChangesAsync(); // Works

            // Not tracked
            var user = await db.Users.AsNoTracking().FirstAsync();
            user.Name = "Martin";
            await db.SaveChangesAsync(); // Does nothing — EF doesn’t know about this object
        */
    [MemoryDiagnoser]
    public class IterationBenchmarks
    {
        private int[] data;

        public IterationBenchmarks()
        {
            data = Enumerable.Range(1, 1000).ToArray();
        }

        [Benchmark]
        public int ForLoop()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        [Benchmark]
        public int ForeachLoop()
        {
            int sum = 0;
            foreach (var item in data)
            {
                sum += item;
            }
            return sum;
        }

        [Benchmark]
        public int SpanLoop()
        {
            var spanData = data.AsSpan();
            int sum = 0;
            for (int i = 0; i < spanData.Length; i++)
            {
                sum += spanData[i];
            }
            return sum;
        }

        [Benchmark]
        public int LinqSum()
        {
            // This uses LINQ's Sum extension - internally optimized
            return data.Sum();
        }

        [Benchmark]
        public int ForeachLoopEven()
        {
            int sum = 0;
            foreach (var item in data)
            {
                if (item % 2 == 0)
                    sum += item;
            }
            return sum;
        }

        [Benchmark]
        public int LinqSumEven()
        {
            // Sum only even numbers without allocations, no ToList/ToArray
            return data.Where(x => x % 2 == 0).Sum();
        }

        [Benchmark]
        public int ForeachLoopSquared()
        {
            int sum = 0;
            foreach (var item in data)
            {
                sum += item * item;
            }
            return sum;
        }

        [Benchmark]
        public int LinqSumSquared()
        {
            // Select squared values and sum them
            return data.Select(x => x * x).Sum();
        }
    }
}
