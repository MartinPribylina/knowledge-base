# Design Patterns in C#

## What Are Design Patterns?

Design Patterns are **proven solutions to common software design problems**. They represent best practices refined by experienced developers, not ready-made code, but conceptual templates for solving design issues.

They improve:
- **Code reusability**
- **Maintainability**
- **Scalability**
- **Communication between developers** (shared vocabulary)

---

## Categories of Design Patterns

Design Patterns are grouped into three main categories:

| Category       | Purpose                                           |
|----------------|---------------------------------------------------|
| Creational     | Deal with object creation                         |
| Structural     | Deal with object composition                      |
| Behavioral     | Deal with object communication and responsibility |

---

## 🏗️ Creational Patterns

### Builder

### Singleton
Ensures a class has only one instance and provides a global point of access.

```csharp
public sealed class Logger
{
    private static readonly Logger _instance = new Logger();
    private Logger() { }
    public static Logger Instance => _instance;
}
```