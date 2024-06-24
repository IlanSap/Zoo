using System;

public class Monkey : Animal
{
    public override int StepSize { get; set; } = 2;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Green;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.White;
    public override AnimalType AnimalType { get; } = AnimalType.Monkey;
}