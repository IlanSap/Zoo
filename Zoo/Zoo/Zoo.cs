using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using ZooProject.AnimalFactory;
using ZooProject.Animals.AnimalTypes;

namespace ZooProject.Zoo;

public class Zoo
{
    public List<Animal> Animals { get; set; } = new List<Animal>();
    public int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    public ZooArea ZooArea;
    public ZooPlot ZooPlot = new ZooPlot();
    public double IntervalSeconds;
    private Timer _moveAnimalsTimer;

    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ZooId { get; set; }

    private readonly GPSTracker _gpsTracker;


    public Zoo()
    {
    }


    public Zoo(GPSTracker gpsTracker)
    {
        _gpsTracker = gpsTracker;
    }


    public void SetZooSize(int size)
    {
        ZooArea = new ZooArea(this, size, _gpsTracker);
    }


    public void SetZooSizeComposite(int size)
    {
        ZooArea = new CompositeZooArea(this, size, _gpsTracker, ZooPlot);
    }


    public int GetAnimalMatrixSize()
    {
        return AnimalMatrixSize;
    }


    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
    }


    public void GenerateAnimals(AnimalType type, int count)
    {
        IAnimalFactory factory = new AnimalFactory.AnimalFactory();
        int numOfSuccessfulPlacements = 0;
        for (int i = 0; i < count; i++)
        {
            Animal animal = factory.CreateAnimal(type);
            if (ZooArea.PlaceAnimal(animal) == false)
            {
                Console.WriteLine($"The {(animal.AnimalType).ToString()} couldn't be placed.");
            }
            else
            {
                AddAnimal(animal);
                numOfSuccessfulPlacements++;
            }
        }
        Console.WriteLine($"{numOfSuccessfulPlacements} out of {count} {type.ToString()}s were placed in the zoo.");
    }


    public void MoveAllAnimals()
    {
         
        foreach (var animal in Animals)
        {
            animal.Move(MoveAnimal);
        }
    }


    public bool MoveAnimal(Animal animal)
    {
        Random rnd = new Random();
        List <Tuple<int, int>> directions = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 1),  // Right
            new Tuple<int, int>(0, -1), // Left
            new Tuple<int, int>(-1, 0), // Up
            new Tuple<int, int>(1, 0)   // Down
        };
        // Multiply directions by the animal step size
        for (int i = 0; i < directions.Count; i++)
        {
            directions[i] = new Tuple<int, int>(directions[i].Item1 * animal.StepSize, directions[i].Item2 * animal.StepSize);
        }
        var (currentRow, currentCol) = _gpsTracker.GetPosition(animal.AnimalId);
        bool moved = false;

        while (directions.Count > 0 && !moved)
        {
            int index = rnd.Next(directions.Count);
            var (dirRow, dirCol) = directions[index];
            directions.RemoveAt(index);

            int newRow = currentRow + dirRow * this.GetAnimalMatrixSize();
            int newCol = currentCol + dirCol * this.GetAnimalMatrixSize();

            ZooArea zooArea = ZooArea;

            if (ZooArea is CompositeZooArea _compositeZooArea)
            {
                zooArea = _compositeZooArea.Areas[animal.AnimalType];
            }

            if (newRow >= 0 && newRow <= zooArea.ZooMap.Length - this.GetAnimalMatrixSize() &&
                newCol >= 0 && newCol <= zooArea.ZooMap[0].Length - this.GetAnimalMatrixSize() &&
                zooArea.CheckIfEmpty(animal, newRow, newCol))
            {
                zooArea.ClearAnimalPosition(animal, currentRow, currentCol);
                zooArea.InsertAnimal(animal, newRow, newCol);
                zooArea.UpdateRowAndColArrays(animal, currentRow, currentCol, newRow, newCol);

                if (zooArea is CompositeZooArea compositeZooArea)
                {
                    currentRow += compositeZooArea.AreaStartRow[compositeZooArea.Areas[animal.AnimalType]];
                    newRow += compositeZooArea.AreaStartRow[compositeZooArea.Areas[animal.AnimalType]];
                }

                ZooArea.UpdateSpecificCellsAfterAnimalMove(animal, zooArea, currentRow, currentCol, newRow, newCol);

                moved = true;
            }
        }
        if (!moved)
        {
            //Console.WriteLine($"{animal.AnimalType.ToString()} couldn't find a place to move.");
        }
        return moved;
    }


    public bool CanFitAnimals(int animalCount)
    {
        int availableSpace = (ZooArea.ZooMap.Length * ZooArea.ZooMap[0].Length) / (AnimalMatrixSize * AnimalMatrixSize);
        return animalCount <= availableSpace;
    }


    public void InitializeTimer(double intervalSeconds)
    {
        _moveAnimalsTimer = new Timer(
            callback: _ => MoveAnimalsEvent(),
            state: null,
            dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
            period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
        );
    }


    private void MoveAnimalsEvent()
    {
        try
        {
            this.MoveAllAnimals();
            //Console.WriteLine("Animals moved automatically.");
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"An error occurred while moving animals: {ex.Message}");
        }
    }
}
