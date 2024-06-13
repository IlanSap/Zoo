using System;

public class Zebra : Animal
{
    public override int StepSize { get; set; } = 1;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.White;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Black;
    public override AnimalType AnimalType { get; } = AnimalType.Zebra;
}