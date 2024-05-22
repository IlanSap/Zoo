/*
    There are some redundant stuff here, if you have string Name { get; } you don't need string getName();, again please read about C# properties
*/

public interface IAnimal
{
    void Move();
    string Name { get; }

    // direcations for the animal to move
    List<Tuple<int, int>> MoveDirections { get; }

    System.ConsoleColor AnimalBackgroundColor { get; }
    System.ConsoleColor AnimalForegroundColor { get; }

    string getName();

}