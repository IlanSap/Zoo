using System;
using System.Collections.Generic;


/*
    1.There's no reason for the zoo to be a singleton.
    2.Please utilize SOLID principles, there's no need for the zoo to do everything, think how you can break it up. (Do one thing and do it well)
    3.I don't think that AnimalPosition belongs here, why not having it instide the animal itself?
    4.Utilize OOP principles.
    5.If you don't use this class delete it.
*/

public class SingletonZoo
{
    private static SingletonZoo _instance;
    private List<IAnimal> _animals = new List<IAnimal>();
    private Dictionary<IAnimal, (int row, int col)> _animalPositions = new Dictionary<IAnimal, (int row, int col)>();
    const int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    private char[][] _zooMap;
    private int[] zooRow;
    private int[] zooCol;
    
    // TO-DO: Decide if the AnimalPosition should be in this file or in the IAnimal.cs file. And if it necessary to have a struct for this.
    // sotre the position of the animal in the zoo (top-left corner)
    struct AnimalPosition
    {
        public int row;
        public int col;
    }


    private SingletonZoo() { }

    public static SingletonZoo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SingletonZoo();
            }
            return _instance;
        }
    }

    public void SetZooSize(int size)
    {
        _zooMap = new char[size][];
        for (int i = 0; i < size; i++)
        {
            _zooMap[i] = new char[size];
            Array.Fill(_zooMap[i], ' '); // fill the array with spaces indicating empty spaces.
        }
        zooCol = new int[size];
        Array.Fill(zooCol, 0);
        zooRow = new int[size];
        Array.Fill(zooRow, 0);
    }

    public void AddAnimal(IAnimal animal)
    {
        _animals.Add(animal);
    }

    public void GenerateAnimals(string animalType, int count)
    {
        IAnimalFactory factory;
        switch (animalType.ToLower())
        {
            case "lion":
                factory = new LionFactory();
                break;
            case "monkey":
                factory = new MonkeyFactory();
                break;
            default:
                Console.WriteLine($"Unknown animal type: {animalType}");
                return;
        }

        int numOfSucessfulPlacements = 0;
        for (int i = 0; i < count; i++)
        {
            IAnimal animal = factory.CreateAnimal($"{animalType} {i + 1}");
            //AddAnimal(animal);
            //PlaceAnimal(animal);
            if (PlaceAnimal2(animal) == 1)
            {
                Console.WriteLine($"The {animal.Name} couldn't be placed.");
            }
            else
            {
                AddAnimal(animal);
                numOfSucessfulPlacements++;
            }
        }
        Console.WriteLine($"{numOfSucessfulPlacements} out of {count} {animalType}s were placed in the zoo.");
    }

    public void PlaceAnimal(IAnimal animal)
    {
        Random rnd = new Random();
        bool placed = false;
        while (!placed)
        {
            int row = rnd.Next(_zooMap.Length - AnimalMatrixSize + 1);
            int col = rnd.Next(_zooMap[0].Length - AnimalMatrixSize + 1);

            // Check if the 2x2 area is empty
            bool areaIsEmpty = true;
            for (int i = 0; i < AnimalMatrixSize; i++)
            {
                for (int j = 0; j < AnimalMatrixSize; j++)
                {
                    if (_zooMap[row + i][col + j] != ' ')
                    {
                        areaIsEmpty = false;
                        break;
                    }
                }
                if (!areaIsEmpty) break;
            }

            if (areaIsEmpty)
            {
                for (int i = 0; i < AnimalMatrixSize; i++)
                {
                    for (int j = 0; j < AnimalMatrixSize; j++)
                    {
                        _zooMap[row + i][col + j] = animal is Lion ? 'L' : 'M'; // Simplified for Lion and Monkey
                    }
                }
                _animalPositions[animal] = (row, col); // Track the top-left position of the animal's matrix
                placed = true;
            }
        }
    }

    public int PlaceAnimal2(IAnimal animal)
    {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++) 
        {
            if (zooRow[r] <= colLength - AnimalMatrixSize && zooRow[r+1] <= colLength - AnimalMatrixSize)
            {
                for (int c = 0; c < colLength - 1; c++)
                {
                    if (zooCol[c] <= rowLength - AnimalMatrixSize && zooCol[c + 1] <= rowLength - AnimalMatrixSize)
                    {
                        for (int x=0; x<colLength - 1; x++)
                        {
                            if (_zooMap[x][r]== ' ' && _zooMap[x][r+1] == ' ' && _zooMap[x+1][r] == ' ' && _zooMap[x+1][r+1] == ' ')
                            {
                                _zooMap[x][r] = animal is Lion ? 'L' : 'M';
                                _zooMap[x][r+1] = animal is Lion ? 'L' : 'M';
                                _zooMap[x+1][r] = animal is Lion ? 'L' : 'M';
                                _zooMap[x+1][r+1] = animal is Lion ? 'L' : 'M';
                                zooRow[r] += 2;
                                zooRow[r+1] += 2;
                                zooCol[c] += 2;
                                zooCol[c+1] += 2;
                                _animalPositions[animal] = (x, r);
                                return 0;
                            }   
                        }
                    }
                }
            }
        }
        return 1;
    }

    // Print the row and col arrays, useful for debugging
    public void PrintRowAndColArrays() {         
        Console.WriteLine("Row Array:");
        for (int i = 0; i < zooRow.Length; i++)
        {
            Console.Write(zooRow[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine("Col Array:");
        for (int i = 0; i < zooCol.Length; i++)
        {
            Console.Write(zooCol[i] + " ");
        }
        Console.WriteLine();
    }


    public void MoveAllAnimals()
    {
        int animalCount = _animals.Count;
        Random rnd = new Random();
        foreach (var animal in _animals)
        {
            var (currentRow, currentCol) = _animalPositions[animal];
            bool moved = false;

            // Attempt to move the animal in a random direction (up, down, left, right)
            // Try each direction until a valid move is found or all directions are exhausted
            var directions = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) }; // Up, Down, Left, Right
            int newRow= -1, newCol= -1;
            while (directions.Count > 0 && !moved)
            {
                int index = rnd.Next(directions.Count);
                var (dirRow, dirCol) = directions[index];
                directions.RemoveAt(index); // Remove the attempted direction

                newRow = currentRow + dirRow * AnimalMatrixSize;
                newCol = currentCol + dirCol * AnimalMatrixSize;

                // Check if the new position is within bounds and the 2x2 area is empty
                if (newRow >= 0 && newRow <= _zooMap.Length - AnimalMatrixSize &&
                    newCol >= 0 && newCol <= _zooMap[0].Length - AnimalMatrixSize)
                {
                    bool areaIsEmpty = true;
                    for (int i = 0; i < AnimalMatrixSize && areaIsEmpty; i++)
                    {
                        for (int j = 0; j < AnimalMatrixSize; j++)
                        {
                            if (_zooMap[newRow + i][newCol + j] != ' ')
                            {
                                areaIsEmpty = false;
                                break;
                            }
                        }
                    }

                    if (areaIsEmpty)
                    {
                        // Clear the current position
                        for (int i = 0; i < AnimalMatrixSize; i++)
                        {
                            for (int j = 0; j < AnimalMatrixSize; j++)
                            {
                                _zooMap[currentRow + i][currentCol + j] = ' ';
                            }
                        }

                        // Move the animal to the new position
                        for (int i = 0; i < AnimalMatrixSize; i++)
                        {
                            for (int j = 0; j < AnimalMatrixSize; j++)
                            {
                                _zooMap[newRow + i][newCol + j] = animal is Lion ? 'L' : 'M';
                            }
                        }

                        _animalPositions[animal] = (newRow, newCol); // Update the animal's position
                        moved = true;
                    }
                }
            }

            if (!moved)
            {
                Console.WriteLine($"The {animal.Name} couldn't move.");
                animalCount--;
                if (animalCount == 0)
                {
                    Console.WriteLine("All animals are stuck and can't move.");
                    return;
                }
            }
            else
            {
                if (newRow != -1 || newCol != -1)
                {
                    UpdateRowAndColArrays(currentRow, currentCol, newRow, newCol);
                }
                // Optionally, call the animal's Move method to print the moving message
                //animal.Move();
            }
        }
    }

    public void UpdateRowAndColArrays(int currentRow, int currentCol, int newRow, int newCol)
    {
        if (currentRow == newRow && currentCol == newCol)
        {
            return;
        }
        if (currentRow < 0 || currentCol < 0 || newRow < 0 || newCol < 0 || 
            currentRow >= _zooMap.Length || currentCol >= _zooMap[0].Length || newRow >= _zooMap.Length || newCol >= _zooMap[0].Length)
        {
            return;
        }
        zooCol[currentCol] -= AnimalMatrixSize;
        zooCol[newCol] += AnimalMatrixSize;
        zooRow[currentRow] -= AnimalMatrixSize;
        zooRow[newRow] += AnimalMatrixSize;
    }

    /*
    public void PlotZoo()
    {
        for (int i = 0; i < _zooMap.Length; i++)
        {
            for (int j = 0; j < _zooMap[i].Length; j++)
            {
                Console.Write(_zooMap[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
    */


    public void PlotZoo()
    {
        // Print column indicators
        Console.Write("   "); // Space for row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write($"{col % 10} "); // Use modulo for double-digit numbers
        }
        Console.WriteLine();

        // Print top border
        Console.Write("  +"); // Align with row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write("--");
        }
        Console.WriteLine("+");

        for (int i = 0; i < _zooMap.Length; i++)
        {
            // Print row indicator
            Console.Write($"{i % 10} |"); // Use modulo for double-digit numbers

            for (int j = 0; j < _zooMap[i].Length; j++)
            {
                // Set text color based on the animal type
                switch (_zooMap[i][j])
                {
                    case 'L':
                        Console.ForegroundColor = ConsoleColor.Red; // Lion color
                        break;
                    case 'M':
                        Console.ForegroundColor = ConsoleColor.Green; // Monkey color
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray; // Default color
                        break;
                }

                Console.Write($"{_zooMap[i][j]} ");
                Console.ResetColor(); // Reset to default colors
            }
            Console.WriteLine("|");
        }

        // Print bottom border
        Console.Write("  +"); // Align with row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write("--");
        }
        Console.WriteLine("+");

        // Print legend with colors
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("L - Lion");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("M - Monkey");
        Console.ResetColor();
        Console.WriteLine("  - Empty space");
    }
}