using System;
using System.Threading;
using System.Collections.Generic;


public class ZooManager
{
    public List<Zoo> _zooList = new List<Zoo>();
    public IAnimalFactory _animalFactory;
    public ConsoleHelper _consoleHelper = new ConsoleHelper();
    public int numberOfZoos;


    public ZooManager()
    {
    }

    public void Run()
    {
        IAnimalFactory animalFactory = new AnimalFactory();
        this._animalFactory = animalFactory;
        _consoleHelper.GetInputFromUser(this);

        try
        {
            Console.WriteLine("Starting the Zoo Management System...");
            Console.Clear();
            Console.SetBufferSize(1000,1000);
            for (int i = 0; i < _zooList.Count; i++)
            {
                StartMoveAnimalsTimer(_zooList[i]);
                if (i == _zooList.Count - 1)
                {
                    _zooList[i]._zooPlot.PrintInstructions();
                }
            }
            
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in Run: {ex.Message}");
        }
    }


    public void GenerateRandomAnimals(Zoo zoo, int animalCount)
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
                if (zoo._zooArea.PlaceAnimal(animal) == true)
                {
                    zoo.AddAnimal(animal);
                    remainingAnimals--;
                }
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while generating random animals: {ex.Message}");
        }
    }


    private void StartMoveAnimalsTimer(Zoo zoo)
    {
        try
        {
            if (zoo._moveAnimalsTimer == null)
            {
                double intervalSeconds = zoo._intervalSeconds;
                if (intervalSeconds <= 0)
                {
                    return;
                }

                if (zoo == _zooList[0])
                {
                    zoo._zooPlot.PlotZoo(zoo,0);
                }
                else
                {
                    int startRow = 0;
                    for (int i = 0; i < _zooList.Count; i++)
                    {
                        if (_zooList[i] == zoo)
                            break;
                        startRow += (int) (_zooList[i]._zooArea._zooMap.Length * 1.5);
                    }
                    zoo._zooPlot.PlotZoo(zoo, startRow);
                }

                zoo._moveAnimalsTimer = zoo.InitializeTimer(intervalSeconds);
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
}
