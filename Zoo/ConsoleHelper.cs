using System;

public static class ConsoleHelper
{
    public static void GetInputFromUser(ZooManager zooManager)
    {
        try
        {
            Console.WriteLine("Enter Zoo Size:");
            if (!int.TryParse(Console.ReadLine(), out int zooSize) || zooSize <= 0)
            {
                Console.WriteLine("Invalid input for Zoo Size. Please enter a positive integer.");
                return;
            }

            Console.WriteLine("Enter number of animals to place in the zoo:");
            if (!int.TryParse(Console.ReadLine(), out int animalCount) || animalCount <= 0)
            {
                Console.WriteLine("Invalid input for number of animals. Please enter a positive integer.");
                return;
            }

            Zoo zoo = new Zoo();
            zooManager._zoo = zoo;
            zoo.SetZooSize(zooSize);

            IAnimalFactory animalFactory = new AnimalFactory();
            zooManager._animalFactory = animalFactory;

            if (zoo.CanFitAnimals(animalCount))
            {
                zooManager.GenerateRandomAnimals(animalCount);
            }
            else
            {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static double GetTimeInterval()
    {
        Console.WriteLine("Enter the interval for moving animals (in seconds):");
        if (!double.TryParse(Console.ReadLine(), out double intervalSeconds) || intervalSeconds <= 0)
        {
            Console.WriteLine("Invalid input for interval. Please enter a positive number.");
            return -1;
        }
        return intervalSeconds;
    }
}   