using Common;

namespace Performance.DataTypes
{
    public class HashSetType
    {
        /*
        Source: https://www.geeksforgeeks.org/c-sharp/hashset-in-c-sharp-with-examples/

        HashSet
            Unordered collection of unique elements. Generally used when we want to prevent duplicates in collection.
            Average instruction time O(1) for fundamental operations like adding, removing and checking presence of element. 
            In case of poor hash table worst case scenario O(n).

        Important points:
            Order of elements is not defined
            Homogeneous type can store only one type of values per set.

        Functions:
            Initialize: 
                var hashSet = new HashSet<type>();
            Add value:
                hashSet.Add(value);
            Remove:
                hashSet.Remove(value); // Removes single element (if exist) specified by value
                hashSet.RemoveWhere(Predicate); // Removes all elements that match conditions defined in the predicate
                hashSet.Clear(); // Removes all elements
            Contains value:
                hashSet.Contains(value);
            Set Operations:
                set1.UnionWith(set2); // Combines elements from both
                set1.IntersectWith(set2); // Only elements which are present in both
                set1.ExceptWith(set2); // Elements which are present in 1 and not in 2
         */
        public static void Execute()
        {
            HashSet<int> numbers = new HashSet<int>();

            for (int i = 0; i < 3; i++)
            {
                numbers.Add(i);
            }

            foreach (var num in numbers)
            {
                Logger.Log($"HashSet Contains: {num}");
            }

            Logger.Log("Adding the same numbers again.");

            for (int i = 0; i < 3; i++)
            {
                numbers.Add(i);
            }

            foreach (var num in numbers)
            {
                Logger.Log($"HashSet Contains: {num}");
            }

            Logger.Log("Checking existing numbers by Contains");

            for (int i = 0; i < 6; i++)
            {
                Logger.Log($"Contains {i}? : {numbers.Contains(i)}");
            }

            Logger.Log("Removing existing number and some non existing.");

            for (int i = 0; i < 6; i++)
            {
                numbers.Remove(i);
            }

            foreach (var num in numbers)
            {
                Logger.Log($"HashSet Contains: {num}");
            }
        }
    }
}
