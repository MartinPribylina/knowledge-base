using static DesignPatterns.Builder.FunctionalBuilderWrapper;

namespace DesignPatterns.Builder
{
    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkingAs(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);
    }

    public class FunctionalBuilderWrapper
    {
        public class Person
        {
            public string Name { get; set; } = "";

            public string Position { get; set; } = "";

            public override string ToString() => $"{Name} is a {Position}";
        }

        public abstract class FunctionalBuilder<TSubject, TSelf> 
            where TSelf : FunctionalBuilder<TSubject, TSelf> 
            where TSubject : new()
        {
            private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();

            public TSelf Do(Action<TSubject> action) => AddAction(action);

            public TSubject Build() => actions.Aggregate(new TSubject(), (p, f) => f(p));

            private TSelf AddAction(Action<TSubject> action)
            {
                actions.Add(p => { action(p); return p; });
                return (TSelf)this;
            }
        }

        public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder> 
        { 
            public PersonBuilder Called(string name) => Do(p => p.Name = name);
        
        }

        //public sealed class PersonBuilder
        //{
        //    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        //    public PersonBuilder Called(string name) => Do(p => p.Name = name);

        //    public PersonBuilder Do(Action<Person> action) => AddAction(action);

        //    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

        //    private PersonBuilder AddAction(Action<Person> action)
        //    {
        //        actions.Add(p => { action(p); return p; });
        //        return this;
        //    }
        //}

        public static void Execute()
        {
            Console.WriteLine("FunctionalBuilder");
            Console.WriteLine("-----------------");
            var person = new PersonBuilder()
                .Called("Martin")
                .WorkingAs("Developer")
                .Build();
            Console.WriteLine(person);
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    
}
