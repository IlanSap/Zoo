using System;
using System.Collections.Generic;
using System.Reflection;


public class Zoo
{
    public List<Animal> _animals = new List<Animal>();
    public int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    public ZooArea _zooArea;
    public ZooPlot _zooPlot = new ZooPlot();
    public Timer _moveAnimalsTimer;
    public double _intervalSeconds;
    public Guid ZooId { get; }= Guid.NewGuid();



    public Zoo(CourserPosition lastCourserPosition)
    {
        this._zooPlot.lastCourserPosition = lastCourserPosition;
    }

    public Zoo()
    {
    }

    public void SetZooSize(int size)
    {
        _zooArea = new ZooArea(this, size);
    }


    public int GetAnimalMatrixSize()
    {
        return AnimalMatrixSize;
    }


    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }


    public void GenerateAnimals(AnimalType type, int count)
    {
        // IAnimalFactory factory = (IAnimalFactory)Activator.CreateInstance(Type.GetType($"{animalType}Factory"));
        IAnimalFactory factory = new AnimalFactory();
        int numOfSucessfulPlacements = 0;
        for (int i = 0; i < count; i++)
        {
            // IAnimal animal = factory.CreateAnimal($"{animalType} {i + 1}");
            Animal animal = factory.CreateAnimal(type);
            if (_zooArea.PlaceAnimal(animal) == false)
            {
                Console.WriteLine($"The {(animal.AnimalType).ToString()} couldn't be placed.");
            }
            else
            {
                AddAnimal(animal);
                numOfSucessfulPlacements++;
            }
        }
        Console.WriteLine($"{numOfSucessfulPlacements} out of {count} {type.ToString()}s were placed in the zoo.");
    }

    public void MoveAllAnimals()
    {
         
        foreach (var animal in _animals)
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
        var (currentRow, currentCol) = GPSTracker.GetPosition(animal.AnimalId);
        bool moved = false;

        while (directions.Count > 0 && !moved)
        {
            int index = rnd.Next(directions.Count);
            var (dirRow, dirCol) = directions[index];
            directions.RemoveAt(index);

            int newRow = currentRow + dirRow * this.GetAnimalMatrixSize();
            int newCol = currentCol + dirCol * this.GetAnimalMatrixSize();

            if (newRow >= 0 && newRow <= this._zooArea._zooMap.Length - this.GetAnimalMatrixSize() &&
                newCol >= 0 && newCol <= this._zooArea._zooMap[0].Length - this.GetAnimalMatrixSize() &&
                this._zooArea.CheckIfEmpty(newRow, newCol))
            {
                this._zooArea.ClearAnimalPosition(currentRow, currentCol);
                this._zooArea.InsertAnimal(animal, newRow, newCol);
                this._zooArea.UpdateRowAndColArrays(currentRow, currentCol, newRow, newCol);
                _zooPlot.UpdateSpecificCellsAfterAnimalMove(this, currentRow, currentCol, newRow, newCol);
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
        int availableSpace = (_zooArea._zooMap.Length * _zooArea._zooMap[0].Length) / (AnimalMatrixSize * AnimalMatrixSize);
        return animalCount <= availableSpace;
    }


    public Timer InitializeTimer(double intervalSeconds)
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

    // pause the timer for 20 seconds
    public void PauseTimer()
    {
        try
        {
            _moveAnimalsTimer.Change(TimeSpan.FromSeconds(20), TimeSpan.FromMilliseconds(-1));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while pausing the timer: {ex.Message}");
        }
    }

    // resume the timer
    public void ResumeTimer()
    {
        try
        {
            _moveAnimalsTimer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(_intervalSeconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while resuming the timer: {ex.Message}");
        }
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
            Console.WriteLine($"An error occurred while moving animals: {ex.Message}");
        }
    }
}
