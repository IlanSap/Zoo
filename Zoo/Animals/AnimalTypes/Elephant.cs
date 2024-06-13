using System;

public class Elephant : Animal
{
    public override int StepSize { get; set; } = 1;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.DarkGray;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Black;
    public override AnimalType AnimalType { get; } = AnimalType.Elephant;
}