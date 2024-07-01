using System;

namespace ZooProject.Animals.AnimalTypes;


public class Zebra : Animal
{
    public int StepSize { get; set; } = 1;
    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.White;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Black;
    public AnimalType AnimalType { get; set; } = AnimalType.Zebra;

    public Zebra() : base(AnimalType.Zebra, 1, ConsoleColor.White, ConsoleColor.Black)
    {
    }
}