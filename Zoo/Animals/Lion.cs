using System;


public class Lion : IAnimal
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
}