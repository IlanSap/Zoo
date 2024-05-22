/*
    There's no need to have factory per animal, you can use factory method for example.
*/

public class LionFactory : IAnimalFactory
{
    public IAnimal CreateAnimal(string name)
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
}