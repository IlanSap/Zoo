using System;

public class Lion : Animal
{
    public override int StepSize { get; set; } = 1;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Red;
    public override AnimalType AnimalType { get; } = AnimalType.Lion;
}   