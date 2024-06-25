using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Animal : IAnimal
{
    public Guid AnimalId { get; set; } = Guid.NewGuid();

    public Guid ZooId { get; set; } // NEW

    public int StepSize { get; set; }

    public System.ConsoleColor AnimalBackgroundColor { get; set; }
    public System.ConsoleColor AnimalForegroundColor { get; set; }

    public AnimalType AnimalType { get; set; }

    public void Move(Func<Animal, bool> moveFn)
    {
        bool success = moveFn.Invoke(this);
        /*        if (!success)
                    Console.WriteLine($"Animal {this.AnimalType.ToString()} couldn't find a place to move.");*/
    }

    public Animal(AnimalType animalType, int stepSize=1, ConsoleColor animalBackgroundColor= ConsoleColor.Black, ConsoleColor animalForegroundColor= ConsoleColor.White)
    {
        AnimalType = animalType;
        StepSize = stepSize;
        AnimalBackgroundColor = animalBackgroundColor;
        AnimalForegroundColor = animalForegroundColor;
    }
}

