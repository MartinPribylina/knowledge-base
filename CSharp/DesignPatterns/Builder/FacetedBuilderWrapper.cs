namespace DesignPatterns.Builder
{
    public class FacetedBuilderWrapper
    {
        public class Person
        {
            // Address
            public string StreetAddress { get; set; }

            public string City { get; set; }

            public string PostalCode { get; set; }

            // Employment
            public string CompanyName { get; set; }

            public string Position { get; set; }

            public int AnnualIncome { get; set; }

            public override string ToString() => $"{StreetAddress}, {City}, {PostalCode}, {CompanyName}, {Position}, {AnnualIncome}";
        }

        // Facade
        public class PersonBuilder
        {
            protected Person person = new Person();

            public PersonJobBuilder Works => new PersonJobBuilder(person);

            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            public Person Build() => person;
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder As(string position)
            {
                person.Position = position;
                return this;
            }

            public PersonJobBuilder Earning(int annualIncome)
            {
                person.AnnualIncome = annualIncome;
                return this;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostalCode(string postalCode)
            {
                person.PostalCode = postalCode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }

        public static void Execute()
        {
            Console.WriteLine("FacetedBuilder");
            Console.WriteLine("--------------");
            var pb = new PersonBuilder();
            var person = pb
                .Lives
                    .At("123 London Road")
                    .In("London")
                    .WithPostalCode("SW12BC")
                .Works
                    .At("Microsoft")
                    .As("Software Engineer")
                    .Earning(1000000)
                .Build();

            Console.WriteLine(person);
            Console.WriteLine();
        }
    }
}
