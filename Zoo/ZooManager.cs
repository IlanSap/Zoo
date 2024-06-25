using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;


public class ZooManager
{
    public List<Zoo> _zooList = new List<Zoo>();
    public IAnimalFactory _animalFactory;
    public ConsoleHelper _consoleHelper = new ConsoleHelper();


    public ZooManager()
    {
    }


    public void Run()
    {
        IAnimalFactory animalFactory = new AnimalFactory();
        this._animalFactory = animalFactory;
        var tracker = new GPSTracker();

        Console.WriteLine();

        int zooCount = _consoleHelper.GetZooCount();
        for (int i = 0; i < zooCount; i++)
        {
            var zoo = new Zoo(tracker);
            int zooSize = _consoleHelper.GetZooSize();
            zoo.SetZooSize(zooSize);
            zoo._intervalSeconds = _consoleHelper.GetTimeInterval();
            int animalCount = _consoleHelper.GetAnimalCount();


            _zooList.Add(zoo);


            if (zoo.CanFitAnimals(animalCount))
            {
                GenerateRandomAnimals(zoo, animalCount);
            }
            else
            {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }

            Console.WriteLine();
        }

        try
        {
            //Console.WriteLine("Starting the Zoo Management System...");
            InitialPlotRegular();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
        }
        catch
            (Exception ex)
        {
            Console.WriteLine($"An error occurred in Run: {ex.Message}");
        }
    }


    public void RunWithComposite()
    {
        IAnimalFactory animalFactory = new AnimalFactory();
        this._animalFactory = animalFactory;
        var tracker = new GPSTracker();

        Console.WriteLine();

        int zooCount = _consoleHelper.GetZooCount();
        for (int i = 0; i < zooCount; i++)
        {
            var zoo = new Zoo(tracker);
            int zooSize = 25;
            zoo.SetZooSizeComposite(zooSize);
            zoo._intervalSeconds = _consoleHelper.GetTimeInterval();
            int animalCount = _consoleHelper.GetAnimalCount();

            _zooList.Add(zoo);

            if (zoo.CanFitAnimals(animalCount))
            {
                GenerateRandomAnimals(zoo, animalCount);
            }
            else
            {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }
            Console.WriteLine();
        }

        try
        {
            InitialPlotComposite();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
        }
        catch
            (Exception ex)
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


    private void InitialPlotComposite()
    {
        Console.Clear();
        Console.SetBufferSize(1000, 1000);

        int marginSize = 5;
        int startRow = 0;
        for (int i = 0; i < _zooList.Count; i++)
        {
            var v = _zooList[i]._zooArea as CompositeZooArea;
            v.PlotAreas();
            _zooList[i].InitializeTimer(_zooList[i]._intervalSeconds);

            if (i == _zooList.Count - 1)
            {
                _zooList[i]._zooPlot.PrintOneLegend(_zooList);
                _zooList[i]._zooPlot.PrintInstructions();
            }
        }
    }


    private void InitialPlotRegular()
    {
        Console.Clear();
        Console.SetBufferSize(1000, 1000);

        int marginSize = 5;
        int startRow = 0;
        for (int i = 0; i < _zooList.Count; i++)
        {
            //Console.WriteLine($"Zoo #{i + 1}:");
            _zooList[i]._zooPlot.zooStartRow = startRow;
            _zooList[i]._zooPlot.PlotZoo(_zooList[i]._zooArea, startRow);
            startRow += (int)(_zooList[i]._zooArea._zooMap.Length + 3 + marginSize);
            _zooList[i].InitializeTimer(_zooList[i]._intervalSeconds);

            if (i == _zooList.Count - 1)
            {
                _zooList[i]._zooPlot.PrintOneLegend(_zooList);
                _zooList[i]._zooPlot.PrintInstructions();
            }
        }
    }
}
