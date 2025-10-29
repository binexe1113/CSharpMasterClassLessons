using System;
using System.Collections.Generic;

namespace Coding.Exercise
{
    public class Program
    {
        static void Main(string[] args)
        {
            Exercise exercise = new Exercise();
            var result = exercise.GetCountsOfAnimalsLegs();

            foreach (var legs in result)
            {
                Console.WriteLine(legs);
            }
        }

    }

    public class Exercise
    {
        public List<int> GetCountsOfAnimalsLegs()
        {
            var animals = new List<Animal>
        {
            new Lion(),
            new Tiger(),
            new Duck(),
            new Spider()
        };

            var result = new List<int>();
            foreach (var animal in animals)
            {
                result.Add(animal.NumberOfLegs);
            }
            return result;
        }
    }

    public class Animal
    {
        public virtual int NumberOfLegs { get; } = 4;
    }

    public class Lion : Animal
    {
        // Inherits NumberOfLegs = 4 from base
    }

    public class Tiger : Animal
    {
        // Inherits NumberOfLegs = 4 from base
    }

    public class Duck : Animal
    {
        public override int NumberOfLegs { get; } = 2;
    }

    public class Spider : Animal
    {
        public override int NumberOfLegs { get; } = 8;
    }
}