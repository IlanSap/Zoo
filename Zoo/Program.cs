using System;
using System.Collections.Generic;
using static Zoo;

// TO_DO: Maybe- allow user to add new animal types to the zoo

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Zoo Size:");
        if (!int.TryParse(Console.ReadLine(), out int zooSize) || zooSize <= 0)
        {
            Console.WriteLine("Invalid input for Zoo Size. Please enter a positive integer.");
            return;
        }

        Zoo zoo = new Zoo(); // Directly instantiate the Zoo class
        zoo.SetZooSize(zooSize);

        System.Threading.Timer moveAnimalsTimer = null; // Declare the timer but don't initialize it yet

        while (true)
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
/*              case 3:
                    AddNewAnimalTypeToZoo(zoo);
                    continue;*/
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
        }
    }


    static void AddAnimalToZoo(Zoo zoo, AnimalType type)
    {
        IAnimalFactory factory = new AnimalFactory();
        IAnimal animal = factory.CreateAnimal(type);
        zoo.AddAnimal(animal);
        zoo.PlaceAnimal2(animal);
        zoo.PlotZoo();
    }

    // TO_DO: Change the Generate Animals method and the related function so they will work with the updated classes
    static void GenerateAnimalsInZoo(Zoo zoo)
    {
        Console.WriteLine("Enter animal type (Lion/Monkey):");
        string animalType = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(animalType) ||
                !(Enum.IsDefined(typeof(AnimalType), animalType)))
        {
            Console.WriteLine("Invalid input for animal type. Please enter 'Lion' or 'Monkey'.");
            return;
        }
        Console.WriteLine("Enter number of animals to generate:");
        if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
        {
            Console.WriteLine("Invalid input for number of animals. Please enter a positive integer.");
            return;
        }
        AnimalType type = (AnimalType)Enum.Parse(typeof(AnimalType), animalType);
        zoo.GenerateAnimals(type, count);
        zoo.PlotZoo();
    }

    static void StartMoveAnimalsTimer(Zoo zoo, ref System.Threading.Timer moveAnimalsTimer)
    {
        if (moveAnimalsTimer == null)
        {
            Console.WriteLine("Enter the interval for moving animals (in seconds):");
            if (!int.TryParse(Console.ReadLine(), out int intervalSeconds) || intervalSeconds <= 0)
            {
                Console.WriteLine("Invalid input for interval. Please enter a positive integer.");
                return;
            }
            Console.Clear();
            zoo.PlotZoo();
            moveAnimalsTimer = InitializeTimer(zoo, intervalSeconds);
        }
        else
        {
            Console.WriteLine("Timer is already running.");
        }
    }

    static System.Threading.Timer InitializeTimer(Zoo zoo, int intervalSeconds)
    {
        return new System.Threading.Timer(
            callback: _ => MoveAnimalsEvent(zoo),
            state: null,
            dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
            period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
        );
    }


    static void MoveAnimalsEvent(Zoo zoo)
    {
        zoo.MoveAllAnimals();
        //Console.WriteLine("Animals moved automatically.");
    }


    /*    static void AddNewAnimalTypeToZoo(Zoo zoo)
        {
            Console.WriteLine("Enter animal type:");
            string newAnimalType = Console.ReadLine();
            if (string.IsNullOrEmpty(newAnimalType))
            {
                Console.WriteLine("Invalid input for animal type. Please enter a non-empty string.");
                continue;
            }
            zoo.AddNewAnimalType(newAnimalType);
            Console.WriteLine($"New animal type '{newAnimalType}' added to the zoo.");
        }*/
}