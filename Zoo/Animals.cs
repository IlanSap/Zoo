using System;

public class Lion : IAnimal
{
    public string Name { get; private set; }

    // set MoveDirections for Lion: (0, 1), (0, -1), (-1, 0), (1, 0)
    public List<Tuple<int, int>> MoveDirections { get; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(-1, 0),
        new Tuple<int, int>(1, 0)
    };  

    public Lion(string name)
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine("The Lion is moving.");
    }

    public string getName()
    {
        return Name;
    }

    public ConsoleColor AnimalBackgroundColor { get; } = ConsoleColor.Yellow;
    public ConsoleColor AnimalForegroundColor { get; } = ConsoleColor.Red;
}

public class Monkey : IAnimal
{
    public string Name { get; private set; }

    // set MoveDirections for Monkey: (0, 2), (0, -2), (-2, 0), (2, 0)
    public List<Tuple<int, int>> MoveDirections { get; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0, 2),
        new Tuple<int, int>(0, -2),
        new Tuple<int, int>(-2, 0),
        new Tuple<int, int>(2, 0)
    };

    public Monkey(string name)
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine("The Monkey is moving.");
    }

    public string getName()
    {
        return Name;
    }

    public ConsoleColor AnimalBackgroundColor { get; } = ConsoleColor.Green;
    public ConsoleColor AnimalForegroundColor { get; } = ConsoleColor.Black;
}