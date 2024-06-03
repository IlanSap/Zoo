using System;

public class Monkey : IAnimal
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
}