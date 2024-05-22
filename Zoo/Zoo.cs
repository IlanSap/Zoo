using System;
using System.Collections.Generic;
using System.Reflection;


/*
    1.Please don't use hardcoded stuff.
    2.Why do you need both animal positions and cursor poisitions?
    4.I readlly don't think you need all these data structures to maintain the zoo state, try to be more efficient.
    3.CourserPosition is identical to animal position.
    4.Please return bool for success/fail operations, there's no need to return int.
    5.if you use this, Activator.CreateInstance(Type.GetType($"{animalType}Factory")), you don't really need factory do you?
    6.Please use somthing like Enum, you know which animal types you have.
*/

public class Zoo
{
    private List<IAnimal> _animals = new List<IAnimal>();
    private Dictionary<IAnimal, (int row, int col)> _animalPositions = new Dictionary<IAnimal, (int row, int col)>();
    const int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    private char[][] _zooMap;
    private CourserPosition[][] _zooCourserPositions;
    private CourserPosition lastCourserPosition;
    private int[] zooRow;
    private int[] zooCol;
    private int CoursorTop = 0;
    private int CoursorLeft = 0;
    

    // Map Between char and animal type
    private Dictionary<char, IAnimal> _animalTypeMap = new Dictionary<char, IAnimal>
    {
        { 'L', new Lion("Lion") },
        { 'M', new Monkey("Monkey") }
    };


    struct CourserPosition
    {
        public int row; // Console.CursorTop
        public int col; // Console.CursorLeft
    }

    
    public void SetZooSize(int size)
    {
        _zooMap = new char[size][];
        _zooCourserPositions = new CourserPosition[size][];

        for (int i = 0; i < size; i++)
        {
            _zooMap[i] = new char[size];
            Array.Fill(_zooMap[i], ' '); // fill the array with spaces indicating empty spaces.
       
            _zooCourserPositions[i] = new CourserPosition[size]; // Initialize the inner array
            Array.Fill(_zooCourserPositions[i], new CourserPosition { row = 0, col = 0 }); // Now you can fill it

        }
        zooCol = new int[size];
        Array.Fill(zooCol, 0);
        zooRow = new int[size];
        Array.Fill(zooRow, 0);
    }

 
    public char[][] getZooMap()
    {
        return _zooMap;
    }


    public void AddAnimal(IAnimal animal)
    {
        _animals.Add(animal);
    }


    public void GenerateAnimals(string animalType, int count)
    {
        if (!_animalTypeMap.ContainsKey(animalType[0]))
        {
            Console.WriteLine($"Unknown animal type: {animalType}");
            return;
        }

        IAnimalFactory factory = (IAnimalFactory)Activator.CreateInstance(Type.GetType($"{animalType}Factory"));
        int numOfSucessfulPlacements = 0;
        for (int i = 0; i < count; i++)
        {
            IAnimal animal = factory.CreateAnimal($"{animalType} {i + 1}");
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


    /*public int PlaceAnimal2(IAnimal animal)
    {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++)
        {
            if (zooRow[r] <= colLength - AnimalMatrixSize && zooRow[r + 1] <= colLength - AnimalMatrixSize)
            {
                for (int c = 0; c < colLength - 1; c++)
                {
                    if (zooCol[c] <= rowLength - AnimalMatrixSize && zooCol[c + 1] <= rowLength - AnimalMatrixSize)
                    {
                        for (int x = 0; x < colLength - 1; x++)
                        {
                            if (_zooMap[x][r] == ' ' && _zooMap[x][r + 1] == ' ' && _zooMap[x + 1][r] == ' ' && _zooMap[x + 1][r + 1] == ' ')
                            {
                                _zooMap[x][r] = animal.getName()[0];
                                _zooMap[x][r + 1] = animal.getName()[0];
                                _zooMap[x + 1][r] = animal.getName()[0];
                                _zooMap[x + 1][r + 1] = animal.getName()[0];
                                zooRow[r] += 2;
                                zooRow[r + 1] += 2;
                                zooCol[c] += 2;
                                zooCol[c + 1] += 2;
                                _animalPositions[animal] = (x, r);
                                return 0;
                            }
                        }
                    }
                }
            }
        }
        return 1;
    }*/


    public int PlaceAnimal2(IAnimal animal)
    {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++)
        {
            for (int c = 0; c < colLength - 1; c++)
            {
                if (CheckIfEmpty(r, c))
                {
                    for (int i = 0; i < AnimalMatrixSize; i++)
                    {
                        for (int j = 0; j < AnimalMatrixSize; j++)
                        {
                            _zooMap[r + i][c + j] = animal.getName()[0];
                        }
                    }
                    _animalPositions[animal] = (r, c);
                    zooRow[r] += 2;
                    zooRow[r + 1] += 2;
                    zooCol[c] += 2;
                    zooCol[c + 1] += 2;
                    return 0;
                }
            }
        }
        return 1;
    }


    public void MoveAllAnimals()
    {
        int animalCount = _animals.Count;
        Random rnd = new Random();
        foreach (var animal in _animals)
        {
            var (currentRow, currentCol) = _animalPositions[animal];
            bool moved = false;

            var directions = new List<Tuple<int, int>>(animal.MoveDirections);
            while (directions.Count > 0 && !moved)
            {
                int index = rnd.Next(directions.Count);
                var (dirRow, dirCol) = directions[index];
                directions.RemoveAt(index);

                int newRow = currentRow + dirRow * AnimalMatrixSize;
                int newCol = currentCol + dirCol * AnimalMatrixSize;

                if (newRow >= 0 && newRow <= _zooMap.Length - AnimalMatrixSize &&
                    newCol >= 0 && newCol <= _zooMap[0].Length - AnimalMatrixSize &&
                    CheckIfEmpty(newRow, newCol))
                {
                    ClearAnimalPosition(animal, currentRow, currentCol);
                    InsertAnimal(animal, newRow, newCol);
                    UpdateRowAndColArrays(currentRow, currentCol, newRow, newCol);
                    UpdateSpecificCellsAfterAnimalMove2(currentRow, currentCol, newRow, newCol);
                    moved = true;
                    Console.SetCursorPosition(lastCourserPosition.col, lastCourserPosition.row + 9);
                }
            }

            if (!moved)
            {
                animalCount--;
                if (animalCount == 0) return;
            }
        }
    }



    /// /////////////////////////////////////////////////////////////////

    public void PlotZoo()
    {
        // Save the original console colors to restore them later
        var originalBackgroundColor = Console.BackgroundColor;
        var originalForegroundColor = Console.ForegroundColor;

        // Print column indicators with improved spacing
        Console.Write("    "); // Space for row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write($"{col % 10,2} "); // Use modulo for double-digit numbers, with padding for alignment
            //Console.Write($"{col,2} ");
        }
        Console.WriteLine();

        // Print top border with improved visibility
        Console.Write("   +"); // Align with row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write("---");
        }
        Console.WriteLine("+");

        for (int i = 0; i < _zooMap.Length; i++)
        {
            // Print row indicator with padding for alignment
            Console.Write($"{i % 10,3} |"); // Use modulo for double-digit numbers
            //Console.Write($"{i,3} |");

            for (int j = 0; j < _zooMap[i].Length; j++)
            {
                _zooCourserPositions[i][j] = new CourserPosition { row = Console.CursorTop, col = Console.CursorLeft };
                // Set text and background color based on the animal type
                char animalChar = _zooMap[i][j];

                animalChar = GetMappedAnimalChar(animalChar);
                Console.ForegroundColor = GetAnimalForegroundColor(animalChar);
                Console.BackgroundColor = GetAnimalBackgroundColor(animalChar);

                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
            Console.WriteLine("|");
        }

        // Print bottom border
        Console.Write("   +"); // Align with row indicators
        for (int col = 0; col < _zooMap[0].Length; col++)
        {
            Console.Write("---");
        }
        Console.WriteLine("+");

        // Print legend with colors and ASCII characters
        Console.WriteLine("Legend:");
        Console.ForegroundColor = ConsoleColor.Red;
        //Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" L - Lion");
        Console.ForegroundColor = ConsoleColor.White;
        //Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(" M - Monkey");
        Console.ResetColor();
        Console.WriteLine(" . - Empty space");
        Console.WriteLine();
        lastCourserPosition = new CourserPosition { row = Console.CursorTop, col = Console.CursorLeft };

        // Restore the original console colors
        Console.BackgroundColor = originalBackgroundColor;
        Console.ForegroundColor = originalForegroundColor;
    }




    public void UpdateSpecificCellsAfterAnimalMove2(int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        ClearSpecificCells(oldRow, oldCol);

        // Update the new position
        UpdateSpecificCells(newRow, newCol);
    }

    public void ClearSpecificCells(int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = _zooCourserPositions[row+i][col+j].row;
                int consoleCol = _zooCourserPositions[row+i][col+j].col;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" . "); // Clear the 2x2 space
            }
        }
    }
    
    public void UpdateSpecificCells(int row, int col)
    {
        char animalChar = _zooMap[row][col];
        ConsoleColor foregroundColor, backgroundColor;

        // Determine the char and colors  based on the animal type
        animalChar = GetMappedAnimalChar(animalChar);
        foregroundColor = GetAnimalForegroundColor(animalChar);
        backgroundColor = GetAnimalBackgroundColor(animalChar);

        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = _zooCourserPositions[row + i][col + j].row;
                int consoleCol = _zooCourserPositions[row + i][col + j].col;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////
    /////////////////////// MoveAllAnimals Helper Functions //////////////////////////
    public bool CheckIfEmpty(int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                if (_zooMap[row + i][col + j] != ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ClearAnimalPosition(IAnimal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                _zooMap[row + i][col + j] = ' ';
            }
        }
    }

    public void InsertAnimal(IAnimal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                _zooMap[row + i][col + j] = animal.getName()[0]; // use the first letter of the animal's name
            }
        }
        _animalPositions[animal] = (row, col); // track the top-left position of the animal's matrix
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


    /////////////////////////////////////// Helper Functions ///////////////////////////////////////
    public ConsoleColor GetAnimalForegroundColor(char animalChar)
    {
        return _animalTypeMap.ContainsKey(animalChar) ? _animalTypeMap[animalChar].AnimalForegroundColor : ConsoleColor.Gray;
    }

    public ConsoleColor GetAnimalBackgroundColor(char animalChar)
    {
        return _animalTypeMap.ContainsKey(animalChar) ? _animalTypeMap[animalChar].AnimalBackgroundColor : ConsoleColor.Black;
    }

    public char GetMappedAnimalChar(char animalChar)
    {
        return _animalTypeMap.ContainsKey(animalChar) ? animalChar : '.';
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////

// TO_DO: Delete this function
// Print the row and col arrays, useful for debugging
/*    public void PrintRowAndColArrays()
    {
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
    }*/


/*public void AddNewAnimalType(string animalType)
{
    if (_animalTypeMap.ContainsKey(animalType[0]))
    {
        Console.WriteLine($"Animal type '{animalType}' already exists.");
        return;
    }

    Type factoryType = Type.GetType($"{animalType}Factory");
    if (factoryType == null)
    {
        Console.WriteLine($"Factory for animal type '{animalType}' not found.");
        return;
    }

    IAnimalFactory factory = (IAnimalFactory)Activator.CreateInstance(factoryType);
    IAnimal animal = factory.CreateAnimal(animalType);
    _animalTypeMap.Add(animalType[0], animal);
}*/


/////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Version without using additional array to store the console positions, but not working properly
/*    public void UpdateSpecificCellsAfterAnimalMove(int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        ClearSpecificCells(oldRow, oldCol);

        // Update the new position
        UpdateSpecificCells(newRow, newCol);
    }

    private void ClearSpecificCells(int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = CoursorTop + (row + i);
                int consoleCol = CoursorLeft + (col + j); // Each cell takes up 4 characters in width
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" . "); // Clear the 2x2 space
            }
        }
    }

    private void UpdateSpecificCells(int row, int col)
    {
        char animalChar = _zooMap[row][col];
        ConsoleColor foregroundColor, backgroundColor;

        // Determine the char and colors based on the animal type
        foregroundColor = GetAnimalForegroundColor(animalChar);
        backgroundColor = GetAnimalBackgroundColor(animalChar);

        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = CoursorTop + (row + i);
                int consoleCol = CoursorLeft + (col + j); // Each cell takes up 4 characters in width
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
        }
    }*/