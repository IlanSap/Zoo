using System;

public class Lion : IAnimal
{
    public string Name { get; private set; }

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