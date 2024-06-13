using System;

public class Penguin : Animal
{
    public override int StepSize { get; set; } = 1;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.DarkBlue;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Cyan;
    public override AnimalType AnimalType { get; } = AnimalType.Penguin;
}