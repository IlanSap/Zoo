using System;
// using System.Collections.Generic;
// using static Zoo;

// TO_DO: Maybe- allow user to add new animal types to the zoo

class Program
{
    static void Main(string[] args)
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
            zoo.SetZooSize(zooSize);

            IAnimalFactory animalFactory = new AnimalFactory();
            ZooManager zooManager = new ZooManager(zoo, animalFactory);

            if (zoo.CanFitAnimals(animalCount))
            {
                zooManager.GenerateRandomAnimals(animalCount);
                zooManager.Run();
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
}



/*while (true)
{
    Console.WriteLine("Zoo Managment System");
    Console.WriteLine("1. Add Lion");
    Console.WriteLine("2. Add Monkey");
    Console.WriteLine("3. Move All Animals");
    Console.WriteLine("4. Generate Animals");
    Console.WriteLine("5. Start Moving Animals Timer");
    Console.WriteLine("6. EXIT");
    //Console.WriteLine("_. Add new type of animal");
    Console.Write("Select an option: ");
    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
        continue;
    }

    switch (option)
    {
        case 1:
            AddAnimalToZoo(zoo, AnimalType.Lion);
            continue;
        case 2:
            AddAnimalToZoo(zoo, AnimalType.Monkey);
            continue;
        *//*              case 3:
                            AddNewAnimalTypeToZoo(zoo);
                            continue;*//*
        case 3:
            zoo.MoveAllAnimals();
            zoo.PlotZoo();
            continue;
        case 4:
            GenerateAnimalsInZoo(zoo);
            continue;
        case 5:
            StartMoveAnimalsTimer(zoo, ref moveAnimalsTimer);
            continue;
        case 6:
            moveAnimalsTimer?.Dispose();
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid Option");
            continue;
    }
}*/