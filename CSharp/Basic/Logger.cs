using System.Diagnostics;

namespace Basic
{
    public static class Logger
    {
        public static void Log(string message)
        {
            // Get the calling class name
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1); // 1 = caller of Log()
            var method = frame?.GetMethod();
            var className = method?.DeclaringType?.Name ?? "UnknownClass";

            // Print class name in green
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{className}] ");

            // Reset color and print message
            Console.ForegroundColor = originalColor;
            Console.WriteLine(message);
        }
    }
}
