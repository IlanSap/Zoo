/*public interface IAnimalFactory
{
    IAnimal CreateAnimal(string name);
}*/

public interface IAnimalFactory
{
    IAnimal CreateAnimal(AnimalType type);
}