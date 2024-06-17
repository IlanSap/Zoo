public interface IAnimal
{
    Guid AnimalId { get;}

    public int StepSize { get; set; }

    public System.ConsoleColor AnimalBackgroundColor { get; }
    public System.ConsoleColor AnimalForegroundColor { get; }

    public AnimalType AnimalType { get; }

    public void Move(Func<Animal, bool> moveFn);
}

