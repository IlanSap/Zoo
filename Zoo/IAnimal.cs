public interface IAnimal
{
    void Move();
    string Name { get; }

    System.ConsoleColor AnimalBackgroundColor { get; }
    System.ConsoleColor AnimalForegroundColor { get; }

    string getName();
}