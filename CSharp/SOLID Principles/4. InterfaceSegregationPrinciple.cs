using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Principles
{
    // 🔹 I - Interface Segregation Principle

    // "Clients should not be forced to depend upon interfaces that they do not use."

    // Create smaller, more specific interfaces rather than large,
    // general-purpose ones. Classes should implement only what they need.

    // Bad
    // Fat interface forces unused implementations
    public interface IWorker
    {
        void Work();
        void Eat();
        void Sleep();
    }

    public class Robot : IWorker
    {
        public void Work() => Console.WriteLine("Robot working");

        public void Eat()
        {
            throw new NotSupportedException("Robots don't eat");
        }

        public void Sleep()
        {
            throw new NotSupportedException("Robots don't sleep");
        }
    }


    // Correct
    public interface IWorkable
    {
        void Work();
    }

    public interface IEatable
    {
        void Eat();
    }

    public class Human : IWorkable, IEatable
    {
        public void Work() => Console.WriteLine("Working...");
        public void Eat() => Console.WriteLine("Eating...");
    }

    public class Robot : IWorkable
    {
        public void Work() => Console.WriteLine("Robot working...");
    }

}
