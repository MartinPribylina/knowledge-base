using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Principles
{
    // 🔹 L - Liskov Substitution Principle

    // "Objects of a superclass should be replaceable with objects of a subclass
    // without breaking the application."

    // A derived class must be substitutable for its base class.
    // It shouldn't change expected behavior (e.g. by throwing errors,
    // violating expectations, or silently doing something else).

    public abstract class Bird
    {
        public abstract void MakeSound();

        // Violates LSP, because not all birds can fly
        // public abstract void Fly();
    }

    public class Sparrow : Bird
    {
        public override void MakeSound() => Console.WriteLine("Chirp");

        // public override void Fly() => Console.WriteLine("Flying");
    }

    // Violates LSP if used where flying is assumed
    public class Penguin : Bird
    {
        public override void MakeSound() => Console.WriteLine("Honk");

        // Penguins can't fly
        // public override void Fly() => throw new Exception("Penguins can't fly");
    }

}
