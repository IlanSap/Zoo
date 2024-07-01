
using ZooProject.Animals.AnimalTypes;

namespace ZooProject.AnimalFactory;

public interface IAnimalFactory
{
    Animal CreateAnimal(AnimalType type);
}