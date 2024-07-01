using ZooProject.AnimalFactory;
using ZooProject.Animals.AnimalTypes;
using ZooProject.Zoo;
using ZooProject.Data;

namespace ZooProject;

public class ZooManager
{
    private readonly List<Zoo.Zoo> _zooList = new List<Zoo.Zoo>();
    private IAnimalFactory _animalFactory;
    private readonly ConsoleHelper _consoleHelper;
    private readonly ZooService _zooService;

    public ZooManager(ZooService zooService, ConsoleHelper consoleHelper)
    {
        _zooService = zooService;
        _consoleHelper = consoleHelper;
    }

    public void Run()
    {
        IAnimalFactory animalFactory = new AnimalFactory.AnimalFactory();
        this._animalFactory = animalFactory;
        var tracker = new GPSTracker();

        Console.WriteLine();

        int zooCount = _consoleHelper.GetZooCount();
        for (int i = 0; i < zooCount; i++)
        {
            var zoo = new Zoo.Zoo(tracker);
            int zooSize = _consoleHelper.GetZooSize();
            zoo.SetZooSize(zooSize);
            zoo.IntervalSeconds = _consoleHelper.GetTimeInterval();
            int animalCount = _consoleHelper.GetAnimalCount();

            _zooList.Add(zoo);

            // Save the zoo and its animals to the database
            _zooService.AddZoo(zoo);

            if (zoo.CanFitAnimals(animalCount))
            {
                GenerateRandomAnimals(zoo, animalCount);
            }
            else
            {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }

            //_zooService.AddZoo(zoo); 
            _zooService.UpdateZoo(zoo);
            

            Console.WriteLine();
        }

        try
        {
            InitialPlotRegular();

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

    public void RunWithComposite()
    {
        IAnimalFactory animalFactory = new AnimalFactory.AnimalFactory();
        this._animalFactory = animalFactory;
        var tracker = new GPSTracker();

        Console.WriteLine();

        int zooCount = _consoleHelper.GetZooCount();
        for (int i = 0; i < zooCount; i++)
        {
            var zoo = new Zoo.Zoo(tracker);
/*            {
                ZooId = Guid.NewGuid() // Ensure ZooId is unique
            };*/
            int zooSize = 25;
            zoo.SetZooSizeComposite(zooSize);
            zoo.IntervalSeconds = _consoleHelper.GetTimeInterval();
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

            // Save the zoo and its animals to the database
            _zooService.AddZoo(zoo);

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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in Run: {ex.Message}");
        }
    }

    private void GenerateRandomAnimals(Zoo.Zoo zoo, int animalCount)
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
                if (zoo.ZooArea.PlaceAnimal(animal))
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
            var v = _zooList[i].ZooArea as CompositeZooArea;
            v.PlotAreas();
            _zooList[i].InitializeTimer(_zooList[i].IntervalSeconds);

            if (i == _zooList.Count - 1)
            {
                _zooList[i].ZooPlot.PrintOneLegend(_zooList);
                _zooList[i].ZooPlot.PrintInstructions();
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
            _zooList[i].ZooPlot.zooStartRow = startRow;
            _zooList[i].ZooPlot.PlotZoo(_zooList[i].ZooArea, startRow);
            startRow += (int)(_zooList[i].ZooArea.ZooMap.Length + 3 + marginSize);
            _zooList[i].InitializeTimer(_zooList[i].IntervalSeconds);

            if (i == _zooList.Count - 1)
            {
                _zooList[i].ZooPlot.PrintOneLegend(_zooList);
                _zooList[i].ZooPlot.PrintInstructions();
            }
        }
    }
}