using System;

namespace ZooProject.Animals.AnimalTypes;


public class Lion : Animal
{
    public int StepSize { get; set; } = 1;
    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Red;
    public AnimalType AnimalType { get; set; } = AnimalType.Lion;


    public Lion() : base(AnimalType.Lion, 2, ConsoleColor.Yellow, ConsoleColor.Red)
    {
    }
}