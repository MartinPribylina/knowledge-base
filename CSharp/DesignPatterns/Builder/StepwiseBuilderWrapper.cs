namespace DesignPatterns.Builder
{
    public class StepwiseBuilderWrapper
    {
        public enum CarType
        {
            Sedan,
            Coupe,
            Hatchback
        }

        public class Car
        {
            public CarType Type { get; set; }
            public int Wheels { get; set; }
            public int Doors { get; set; }

            public override string ToString()
            {
                return $"{Type} with {Wheels} wheels and {Doors} doors.";
            }
        }

        public interface ISpecifyCarType
        {
            ISpecifyWheelSize OfType(CarType type);
        }

        public interface ISpecifyWheelSize
        {
            ISpecifyDoors WithWheels(int size);
        }

        public interface ISpecifyDoors
        {
            IBuildCar WithDoors(int size);
        }

        public interface IBuildCar
        {
            public Car Build();
        }

        public class CarBuilder
        {
            private class Impl : ISpecifyCarType, ISpecifyWheelSize, ISpecifyDoors, IBuildCar
            {
                private Car car = new Car();
                public Car Build()
                {
                    return car;
                }

                public ISpecifyWheelSize OfType(CarType type)
                {
                    car.Type = type;
                    return this;
                }

                public IBuildCar WithDoors(int size)
                {
                    switch (car.Type)
                    {
                        case CarType.Sedan when size < 4 || size > 5:
                        case CarType.Coupe when size < 2 || size > 4:
                        case CarType.Hatchback when size < 4 || size > 5:
                            throw new ArgumentException($"Invalid size {size} for car type {car.Type}.");
                    }

                    car.Doors = size;
                    return this;
                }

                public ISpecifyDoors WithWheels(int size)
                {
                    switch (car.Type)
                    {
                        case CarType.Sedan when size < 15 || size > 17:
                        case CarType.Coupe when size < 17 || size > 20:
                        case CarType.Hatchback when size < 14 || size > 16:
                            throw new ArgumentException($"Invalid size {size} for car type {car.Type}.");
                    }

                    car.Wheels = size;
                    return this;
                }
            }
            public static ISpecifyCarType Create()
            {
                return new Impl();
            }
        }

        public static void Execute()
        {
            Console.WriteLine("StepwiseBuilder");
            Console.WriteLine("---------------");
            var car = CarBuilder.Create() // ISpecifyCarType
                .OfType(CarType.Sedan)    // ISpecifyWheelSize
                .WithWheels(15)           // ISpecifyDoors
                .WithDoors(5)             // IBuildCar
                .Build();                 // Car

            Console.WriteLine(car);
            Console.WriteLine();
        }
    }
}
