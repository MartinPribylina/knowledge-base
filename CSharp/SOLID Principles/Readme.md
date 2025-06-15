# SOLID Principles in C#

SOLID is an acronym for five principles of object-oriented design intended to make software more maintainable, understandable, and flexible. These principles are often applied in combination when building robust C# applications, especially in clean architecture and domain-driven design.

## 🟦 S — Single Responsibility Principle (SRP)

> "A class should have only one reason to change."

**Explanation:**  
Each class or module should focus on a single task or responsibility. If a class does more than one thing (e.g. validation, logging, data access), it becomes harder to modify, test, and understand.

**In Practice:**
- Avoid classes like `UserManager` that create users, send welcome emails, and write logs.  
- Instead, separate them into `UserService`, `EmailService`, and `LoggingService`.

---

## 🟩 O — Open/Closed Principle (OCP)

> "Software entities should be open for extension, but closed for modification."

**Explanation:**  
You should be able to add new behavior to a class without changing its existing code. This is commonly achieved using interfaces, abstract classes, and polymorphism.

**In Practice:**
- Instead of modifying a `ReportGenerator` class to support new formats, define a `IReportFormatter` interface and implement `PdfReportFormatter`, `HtmlReportFormatter`, etc.
- The generator then works with any formatter implementing the interface, with no changes to the generator code itself.

---

## 🟨 L — Liskov Substitution Principle (LSP)

> "Objects of a superclass should be replaceable with objects of a subclass without breaking the application."

**Explanation:**  
A derived class must be substitutable for its base class. It shouldn't change expected behavior (e.g. by throwing errors, violating expectations, or silently doing something else).

**In Practice:**
- If `Bird` has a method `Fly()`, then `Penguin : Bird` shouldn't inherit it unless it's redefined to prevent misuse.
- If a derived class violates contracts (like throwing `NotImplementedException` for a method it's expected to support), it's a Liskov violation.

---

## 🟥 I — Interface Segregation Principle (ISP)

> "Clients should not be forced to depend upon interfaces that they do not use."

**Explanation:**  
Create smaller, more specific interfaces rather than large, general-purpose ones. Classes should implement only what they need.

**In Practice:**
- Don’t create a fat interface like `IWorker` with `Work()`, `Eat()`, `Sleep()`.
- Instead, use `IWorkable`, `IEatable`, `ISleepable`, so classes like `RobotWorker` don’t have to implement `Eat()` and `Sleep()` unnecessarily.

---

## 🟪 D — Dependency Inversion Principle (DIP)

> "High-level modules should not depend on low-level modules. Both should depend on abstractions."

**Explanation:**  
Your core business logic should not depend on concrete implementations like a specific database class. Instead, depend on interfaces or abstractions, and inject the concrete implementation at runtime.

**In Practice:**
- Instead of `OrderService` creating an `EmailSender` internally, depend on an `IEmailSender` interface.
- The actual `SmtpEmailSender` is injected via dependency injection.

---

## Summary

| Principle | Focus                      | Goal                                 |
|----------|---------------------------|--------------------------------------|
| SRP      | One responsibility         | Easier maintenance                   |
| OCP      | Extend without modifying   | Add features safely                  |
| LSP      | Substitutability           | Avoid broken behavior in inheritance |
| ISP      | Focused interfaces         | Clean, usable APIs                   |
| DIP      | Abstractions over details  | Loose coupling                       |

Use SOLID as a lens when reviewing or refactoring your architecture. It won't apply perfectly everywhere, but it's an excellent foundation for long-term maintainability and testability.
