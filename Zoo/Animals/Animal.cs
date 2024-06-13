using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Animal : IAnimal
{
    public Guid AnimalId { get; } = Guid.NewGuid();

    public abstract int StepSize { get; set; }

    public abstract System.ConsoleColor AnimalBackgroundColor { get; set; }
    public abstract System.ConsoleColor AnimalForegroundColor { get; set; }

    public abstract AnimalType AnimalType { get; }

    public void Move()
    {
        var _zoo = GPSTracker.GetZoo(AnimalId);
        Func<Animal, bool> moveFunc = _zoo.MoveAnimal;
        bool success = moveFunc(this);
        if (!success)
            Console.WriteLine($"Animal {this.AnimalType.ToString()} couldn't find a place to move.");
    }
}
