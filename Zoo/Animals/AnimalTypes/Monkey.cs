using System;

/*public class Monkey : IAnimal
{
    public AnimalType AnimalType { get; } = AnimalType.Monkey;

    public List<Tuple<int, int>> MoveDirections { get; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 2),
        new Tuple<int, int>(0, -2),
        new Tuple<int, int>(-2, 0),
        new Tuple<int, int>(2, 0)
    };

    public ConsoleColor AnimalBackgroundColor { get; } = ConsoleColor.Green;
    public ConsoleColor AnimalForegroundColor { get; } = ConsoleColor.Black;
}*/

/*
public class Monkey : Animal
{
    public AnimalType AnimalType { get; set} = AnimalType.Monkey;

    public List<Tuple<int, int>> MoveDirections { get; set; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 2),
        new Tuple<int, int>(0, -2),
        new Tuple<int, int>(-2, 0),
        new Tuple<int, int>(2, 0)
    };

    public ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Green;
    public ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.Black;

    public Monkey()
    {
        AnimalType = AnimalType.Monkey;
        MoveDirections = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 1),  // Right
            new Tuple<int, int>(0, -1), // Left
            new Tuple<int, int>(-1, 0), // Up
            new Tuple<int, int>(1, 0)   // Down
        };
        AnimalBackgroundColor = ConsoleColor.Green;
        AnimalForegroundColor = ConsoleColor.White;
    }
}
*/

public class Monkey : Animal
{
    public override int StepSize { get; set; } = 2;
    public override ConsoleColor AnimalBackgroundColor { get; set; } = ConsoleColor.Green;
    public override ConsoleColor AnimalForegroundColor { get; set; } = ConsoleColor.White;
    public override AnimalType AnimalType { get; } = AnimalType.Monkey;

}