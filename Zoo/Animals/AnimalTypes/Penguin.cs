using System;

namespace ZooProject.Animals.AnimalTypes;


public class Penguin : Animal
{
    public int StepSize { get; set; } = 1;
    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.DarkBlue;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Cyan;
    public AnimalType AnimalType { get; set; } = AnimalType.Penguin;

    public Penguin() : base(AnimalType.Penguin, 1, ConsoleColor.DarkBlue, ConsoleColor.Cyan)
    {
    }
}