using System;
using System.Threading;
using System.Collections.Generic;


public class ZooManager
{
    public Zoo _zoo;
    public Timer _moveAnimalsTimer;
    public IAnimalFactory _animalFactory;


    public ZooManager(Zoo zoo, IAnimalFactory animalFactory)
    {
        _zoo = zoo;
        _animalFactory = animalFactory;
    }

    public ZooManager()
    {
    }

    public void Run()
    {
        ConsoleHelper.GetInputFromUser(this);

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
                Animal animal = _animalFactory.CreateAnimal(animalType);
                //animal.AnimalZoo = _zoo;
                if (_zoo._zooArea.PlaceAnimal(animal) == true)
                {
                    _zoo.AddAnimal(animal);
                    remainingAnimals--;
                }
            }

            //ZooPlot.PlotZoo(_zoo);
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
                double intervalSeconds = ConsoleHelper.GetTimeInterval();
                if (intervalSeconds == -1)
                {
                    return;
                }
                Console.Clear();
                ZooPlot.PlotZoo(_zoo);
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
}
