using System;
using ZooProject.Animals.AnimalTypes;

namespace ZooProject.AnimalFactory;

public class AnimalFactory : IAnimalFactory
{
    public Animal CreateAnimal(AnimalType type)
    {
        switch (type)
        {
            case AnimalType.Lion:
                return new Lion();
            case AnimalType.Monkey:
                return new Monkey();
            case AnimalType.Elephant:
                return new Elephant();
            case AnimalType.Penguin:
                return new Penguin();
            case AnimalType.Zebra:
                return new Zebra();
            default:
                throw new ArgumentException($"Animal type {type} is not recognized.");
        }
    }
}