using System;

/*
    1.Each Animal should have it's own class.
    2.Common logic should be placed in a base class.
    3.We don't need the name, but that's ok if you want to keep it.
    4.You don't even use the Move() method, it should move the animal.
    5.I'm not sure why the MoveDirections() is not common, if you intended to make the animals move differenly, then Move() is the right place to do that.
    You can and should change the signatures to fit your needs.
    6.Please read about C# properties and use them.
    7.C# naming convention is pascal case, e.g. GetName and not getName.
    8.If the property/method logic is identical for every instance, use static or const.
*/

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