/*
    We don't really need the name, you can accept animal type to create the right animal you want.
*/

public interface IAnimalFactory
{
    IAnimal CreateAnimal(string name);
}