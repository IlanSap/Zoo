using System;

namespace ZooProject.Animals.AnimalTypes;


public class Elephant : Animal
{
    public int StepSize { get; set; } = 1;
    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.DarkGray;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Black;
    public AnimalType AnimalType { get; set; } = AnimalType.Elephant;

    public Elephant() : base(AnimalType.Elephant, 1, ConsoleColor.DarkGray, ConsoleColor.Black)
    {
    }
}