namespace Basic
{
    public class ValueVsReferenceTypes
    {
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }

        public static void Execute()
        {
            unsafe
            {
                // Value Types vs Reference Types
                // Value Types stored in memory, Reference Types stored on heap. Reference stored address to variable on heap on stack.
                // Values Types: int, char, bool, float, double, decimal, Structs, Arrays

                // Variable Types are 
                int x = 10;
                int y = x;
                x = 20;
                Logger.Log($"x: {x} address: {GetAddress((long)&x)} value: {GetValueOnAdress((long)&x, "")}, y: {y} address: {GetAddress((long)&y)} value: {GetValueOnAdress((long)&y, "")}"); // x: 20, y: 10

                // Reference Types: string, Lists, Dictionaries, Objects, Classes
                string str = "Hello";
                string str2 = str;
                str2 += " World!";
                Logger.Log($"str: {str} address: {GetAddress((long)&str)} value: {GetValueOnAdress((long)&str)}, y: {str2} address: {GetAddress((long)&str2)} value: {GetValueOnAdress((long)&str2)}"); // x: 20, y: 10

                Employee a = new Employee { Id = 1, Name = "Gavin Lon" }; // Memmory address is stored on stack and object is stored on heap
                Logger.Log("Address of Employee a: " + GetAddress((long)&a) + " Value of Employee a on Stack: " + GetValueOnAdress((long)&a));

                Employee b = a; // b is a reference variable and points to the same memory address as a
                Logger.Log("Address of Employee b: " + GetAddress((long)&b) + " Value of Employee a on Stack: " + GetValueOnAdress((long)&b));

                a.Name = "David Hasslehof"; // Change is applied to the object stored on heap
                Logger.Log($"Name of Employee a: {a.Name} Name of Employee b: {b.Name}"); // Prints David Hasslehof for both
            }

        }

        private static unsafe string GetAddress(long ptr)
        {
            return ((long)ptr).ToString("X");
        }

        private static unsafe string GetValueOnAdress(long ptr, string formatting = "X")
        {
            int value = *(int*)ptr;
            return value.ToString(formatting);
        }
    }
}
