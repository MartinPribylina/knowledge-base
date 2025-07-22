namespace DesignPatterns.Builder
{
    public class GenericBuilderWrapper
    {
        public class Person
        {
            private string name = "";
            private string position = "";

            public class Builder : PersonJobBuilder<Builder>
            {

            }

            public static Builder New => new Builder();

            public void SetName(string name)
            {
                this.name = name;
            }

            public void SetPosition(string position)
            {
                this.position = position;
            }

            public override string ToString()
            {
                return $"Name: {name}, Position: {position}";
            }
        }
        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
        {
            public SELF Named(string name)
            {
                person.SetName(name);
                return (SELF)this;
            }
        }

        public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorkingAs(string position)
            {
                person.SetPosition(position);
                return (SELF)this;
            }
        }

        public static void Execute()
        {
            Console.WriteLine("GenericBuilder");
            Console.WriteLine("--------------");
            var person = Person.New.WorkingAs("Developer").Named("Martin").Build();
            Console.WriteLine(person);
            Console.WriteLine();
        }
    }
}
