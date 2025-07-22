using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
    public class DataTypes
    {
        public static void Execute()
        {
            // Basic Data Types: int, char, bool, float, double, decimal
            int x = 10;
            char c = 'a';
            bool b = true;
            float f = 1.1f;
            double d = 1.1d;
            decimal dec = 1.1m;
            Logger.Log($"x: {x}, c: {c}, b: {b}, f: {f}, d: {d}, dec: {dec}");


            // Arrays, Lists, Dictionaries, HashSets, Queues, Stacks
            int[] ints = { 1, 2, 3 };
            List<int> list = new List<int> { 1, 2, 3 };
            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            HashSet<int> hashSet = new HashSet<int> { 1, 2, 3 };
            Queue<int> queue = new Queue<int>(new[] { 1, 2, 3 });
            Stack<int> stack = new Stack<int>(new[] { 1, 2, 3 });
        }
    }
}
