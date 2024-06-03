using System;
using System.Threading;
using System.Collections.Generic;

// TO_DO: Maybe- allow user to add new animal types to the zoo


public class ZooManager
{
    private Zoo _zoo;
    private Timer _moveAnimalsTimer;
    private IAnimalFactory _animalFactory;

/*    public ZooManager(Zoo zoo)
    {
        _zoo = zoo;
    }*/

    public ZooManager(Zoo zoo, IAnimalFactory animalFactory)
    {
        _zoo = zoo;
        _animalFactory = animalFactory;
    }

    public void Run()
    {
        try
        {
            Console.WriteLine("Starting the Zoo Management System...");
            StartMoveAnimalsTimer();
            while (true)
            {
                Console.WriteLine("Press 'q' to quit.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    _moveAnimalsTimer?.Dispose();
                    Environment.Exit(0);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in Run: {ex.Message}");
        }
    }


    public void GenerateRandomAnimals(int animalCount)
    {
        try
        {
            Array animalTypes = Enum.GetValues(typeof(AnimalType));
            Random random = new Random();
            int remainingAnimals = animalCount;

            while (remainingAnimals > 0)
            {
                AnimalType animalType = (AnimalType)animalTypes.GetValue(random.Next(animalTypes.Length));
                IAnimal animal = _animalFactory.CreateAnimal(animalType);
                if (_zoo.PlaceAnimal(animal) == 0)
                {
                    _zoo.AddAnimal(animal);
                    remainingAnimals--;
                }
            }

            _zoo.PlotZoo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while generating random animals: {ex.Message}");
        }
    }


    private void StartMoveAnimalsTimer()
    {
        try
        {
            if (_moveAnimalsTimer == null)
            {
                Console.WriteLine("Enter the interval for moving animals (in seconds):");
                if (!double.TryParse(Console.ReadLine(), out double intervalSeconds) || intervalSeconds <= 0)
                {
                    Console.WriteLine("Invalid input for interval. Please enter a positive number.");
                    return;
                }
                Console.Clear();
                _zoo.PlotZoo();
                _moveAnimalsTimer = InitializeTimer(intervalSeconds);
            }
            else
            {
                Console.WriteLine("Timer is already running.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while starting the timer: {ex.Message}");
        }
    }


    private Timer InitializeTimer(double intervalSeconds)
    {
        try
        {
            return new Timer(
                callback: _ => MoveAnimalsEvent(),
                state: null,
                dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
                period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while initializing the timer: {ex.Message}");
            return null;
        }
    }


    private void MoveAnimalsEvent()
    {
        try
        {
            _zoo.MoveAllAnimals();
            //Console.WriteLine("Animals moved automatically.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while moving animals: {ex.Message}");
        }
    }


    // Generate animals in the zoo based on user input for animal type and count of animals to generate
    // Currently not used in the main program (but it works)
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

    // Add an animal to the zoo based on user input for animal type
    // Currently not used in the main program (but it works)
    static void AddAnimalToZoo(Zoo zoo, AnimalType type)
    {
        IAnimalFactory factory = new AnimalFactory();
        IAnimal animal = factory.CreateAnimal(type);
        zoo.AddAnimal(animal);
        zoo.PlaceAnimal(animal);
        zoo.PlotZoo();
    }
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