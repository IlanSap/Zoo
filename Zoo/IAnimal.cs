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