using System;

namespace ZooProject.Animals.AnimalTypes;

public class Monkey : Animal
{
    public int StepSize { get; set; } = 2;
    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Green;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.White;
    public AnimalType AnimalType { get; set; } = AnimalType.Monkey;

    public Monkey() : base(AnimalType.Monkey, 2, ConsoleColor.Green, ConsoleColor.White)
    {
    }
}