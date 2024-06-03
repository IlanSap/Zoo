/*public class LionFactory : IAnimalFactory
{
    public IAnimal CreateAnimal(string name)
    {
        return new Lion(name);
    }

    public IAnimal CreateAnimal(AnimalType animalType)
    {
        return new Lion(animalType.ToString());
    }

    public IAnimal CreateAnimal(AnimalType animalType, string name)
    {
        return new Lion(name);
    }
}

public class MonkeyFactory : IAnimalFactory
{
    public IAnimal CreateAnimal(string name)
    {
        return new Monkey(name);
    }

    public IAnimal CreateAnimal(AnimalType animalType)
    {
        return new Monkey(animalType.ToString());
    }

    public IAnimal CreateAnimal(AnimalType animalType, string name)
    {
        return new Monkey(name);
    }
}*/

using System;

public class AnimalFactory : IAnimalFactory
{
    public IAnimal CreateAnimal(AnimalType type)
    {
        switch (type)
        {
            case AnimalType.Lion:
                return new Lion();
            case AnimalType.Monkey:
                return new Monkey();
            default:
                throw new ArgumentException($"Animal type {type} is not recognized.");
        }
    }
}