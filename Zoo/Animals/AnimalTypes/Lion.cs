using System;


/*public class Lion : IAnimal
{
    public AnimalType AnimalType { get; } = AnimalType.Lion;

    public List<Tuple<int, int>> MoveDirections { get; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(-1, 0),
        new Tuple<int, int>(1, 0)
    };


    public ConsoleColor AnimalBackgroundColor { get; } = ConsoleColor.Yellow;
    public ConsoleColor AnimalForegroundColor { get; } = ConsoleColor.Red;
}*/

/*
public class Lion : Animal
{
    public AnimalType AnimalType { get; set; } = AnimalType.Lion;

    public List<Tuple<int, int>> MoveDirections { get; set; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(-1, 0),
        new Tuple<int, int>(1, 0)
    };

    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Red;

    public Lion()
    {
        AnimalType = AnimalType.Lion;
        MoveDirections = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 1),  // Right
            new Tuple<int, int>(0, -1), // Left
            new Tuple<int, int>(-1, 0), // Up
            new Tuple<int, int>(1, 0)   // Down
        };
        AnimalBackgroundColor = ConsoleColor.Yellow;
        AnimalForegroundColor = ConsoleColor.Red;
    }
}*/

public class Lion : Animal
{
    public override int StepSize { get; set; } = 1;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Red;
    public override AnimalType AnimalType { get; } = AnimalType.Lion;
}   