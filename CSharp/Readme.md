# C#

.NET - enviroment in which C# application run

.NET is a cross platform, modular, agile, fast, robust and secure environment in which your C# applications can run.

C# and .NET have now evolved to a point where you can “write once and run anywhere”.

## Core Common Language Runtime

Provides Just-in-time compilation, memory management, garbage collection, security and exception handling.

## Commands

Mostly not used when working with Visual Studio

Check version

```
dotnet --version
```

Create new console app

```
dotnet new console
```

Run project based on current directory

```
dotnet run
```

## Data Types

Statically typed programming language :heart: (Except dynamic type)

The advantage of strongly typing variables is that potential data type-related errors can be flagged at compiled time and then dealt with appropriately at compile time.

### Value Types and Reference Types

C# data types can be put into two main classifications: value types and reference types. These main data type classifications denote how data for C# data types are stored in memory.

A value type is stored in a memory location called the stack, where the value assigned to a variable is stored in the relevant memory space on the stack.

A reference type is stored in a memory location known as the heap, where an address of where the actual data is stored resides on the stack and points to the location where the actual data is stored on the heap.

A key difference between data stored on the stack and data stored on the heap is that all data stored on the stack has a fixed size, where data stored on the heap does not have a fixed size. the fixed size for discrete data stored on the stack means more efficiency in the storage and retrieval of such data when compared to the management of data stored on the heap.

### Strings

#### Immutability of C# Strings

The difference between the string reference type and other reference types (like, for example, an object instantiated from a class) is that the data for a particular string (stored on the heap) cannot be directly changed in memory. This means that every time, for example, a concatenation operation occurs in code, the memory address stored on the stack is simply amended to point to a new memory location on the heap that stores the new string that has been created as a result of the relevant concatenation operation.

#### Quoted String Literals, Verbatim String Literals and Raw String Literals

```
// Quoted String
// 1 line, 1 double quote start and end
string path = "C:\\development\\CSharpProjects";
Console.Write(path);
// Output: C:\development\CSharpProjects

// Escape character \
string path = "\"C:\\development\\CSharpProjects\"";
Console.WriteLine(path);
//Output: "C:\development\CSharpProjects"

// Verbatum String Literals
string path = @"""C:\development\CSharpProjects""";
Console.WriteLine(path);
// Output: "C:\development\CSharpProjects"

// For multiline text
string narrative =
    @"Humpty Dumpty sat on the wall
Humpty Dumpty had a great fall
all the kings horses and all the kings men
couldn’t put Humpty together again";

// Raw String Literals
string text = """
"To be or not to be" is a quote from Shakespeare's Hamlet.
""";
Console.WriteLine(text);
```

#### Useful C# Built-in String Methods

IndexOf

```
var narrative = "Gavin Lon loves to create free courses on the freeCodeCamp YouTube channel.";

// find freeCodeCamp in the narrative
var indx = narrative.IndexOf("freeCodeCamp");

// the value of indx will be 46
```

Replace

```
var narrative = "Gavin Lon loves to create free courses on the freeCodeCamp YouTube channel.";
var newNarrative = narrative.Replace("Gavin Lon", "Farhan Hassan Chowdury");
Console.WriteLine(newNarrative);
// Output: Farhan Hassan Chowdury loves to create free courses on the freeCodeCamp YouTube channel.
```

Substring

```
var narrative = "Gavin Lon loves to create free courses on the freeCodeCamp YouTube channel.";
var charityName = narrative.Substring(46, 12);
Console.WriteLine(charityName);
// Output: freeCodeCamp
```

## Operators

[Microsoft Operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/)
Most frequent operators:

```
x && y // AND
x || y // OR
x ?? y // Null-coalescing
c ? t : f // Ternary
x & y // bitwise logical AND
x | y // bitwise logical OR
x ^ y // bitwise logical XOR
```

## Constants and Read-only Variables

Constants are used for variables that will never change

# Learning Sources

[freecodecamp](https://www.freecodecamp.org/news/learn-csharp-book/#heading-introduction-to-net)
