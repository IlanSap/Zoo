using System;
using System.Collections.Generic;


public class Zoo
{
    private List<IAnimal> _animals = new List<IAnimal>();
    private Dictionary<IAnimal, (int row, int col)> _animalPositions = new Dictionary<IAnimal, (int row, int col)>();
    const int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    private char[][] _zooMap;
    private CourserPosition[][] _zooCourserPositions;
    private int[] zooRow;
    private int[] zooCol;
    int CoursorTop = 0;
    int CoursorLeft = 0;

    // Map Between char and animal type
    private Dictionary<char, IAnimal> _animalTypeMap = new Dictionary<char, IAnimal>
    {
        { 'L', new Lion("Lion") },
        { 'M', new Monkey("Monkey") }
    };

    //private char[][] _previousZooMap;


    // TO-DO: Decide if the AnimalPosition should be in this file or in the IAnimal.cs file
    // sotre the position of the animal in the zoo (top-left corner)
    struct AnimalPosition
    {
        public int row;
        public int col;
    }

    struct CourserPosition
    {
        public int row;
        public int col;
    }

    /*
    // Public constructor to allow instantiation
    public Zoo() { }

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
    */

    
    public void SetZooSize(int size)
    {
        _zooMap = new char[size][];
        //_previousZooMap = new char[size][];
        _zooCourserPositions = new CourserPosition[size][];
        for (int i = 0; i < size; i++)
        {
            _zooMap[i] = new char[size];
            //_previousZooMap[i] = new char[size];
            Array.Fill(_zooMap[i], ' '); // fill the array with spaces indicating empty spaces.
            //Array.Fill(_previousZooMap[i], '\0'); // fill with null characters to ensure everything is updated the first time.
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


    // TO-DO: Understand the algorithm and improve it, try to think about an implementation in O(n) time complexity
    public int PlaceAnimal2(IAnimal animal)
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
    }

    // Print the row and col arrays, useful for debugging
    public void PrintRowAndColArrays()
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
            int newRow = -1, newCol = -1;
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
                    bool areaIsEmpty = CheckIfEmpty(newRow, newCol);

                    if (areaIsEmpty)
                    {
                        // Clear the current position
                        ClearAnimalPosition(animal, currentRow, currentCol);
                        // Move the animal to the new position
                        InsertAnimal(animal, newRow, newCol);
                        moved = true;
                    }
                }
            }

            if (!moved)
            {
                //Console.WriteLine($"The {animal.Name} couldn't move.");
                animalCount--;
                if (animalCount == 0)
                {
                    //Console.WriteLine("All animals are stuck and can't move.");
                    return;
                }
            }
            else
            {
                if (newRow != -1 || newCol != -1)
                {
                    //Console.WriteLine($"({currentRow}, {currentCol}) => ({newRow}, {newCol}).");
                    UpdateRowAndColArrays(currentRow, currentCol, newRow, newCol);
                    //UpdateSpecificCellsAfterAnimalMove(currentRow, currentCol, newRow, newCol);
                    UpdateSpecificCellsAfterAnimalMove2(currentRow, currentCol, newRow, newCol);
                }
                // Optionally, call the animal's Move method to print the moving message
                //animal.Move();
            }
        }
    }


    /// //////////////////// MoveAllAnimals Helper Functions //////////////////////////
    public bool CheckIfEmpty(int row, int col)
    {
        bool areaIsEmpty = true;
        for (int i = 0; i < AnimalMatrixSize && areaIsEmpty; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                if (_zooMap[row + i][col + j] != ' ')
                {
                    areaIsEmpty = false;
                    break;
                }
            }
        }
        return areaIsEmpty;
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

            for (int j = 0; j < _zooMap[i].Length; j++)
            {
                //if (i == 0 && j == 0)
                //{
                //    CoursorLeft = Console.CursorLeft;
                //    CoursorTop = Console.CursorTop;
                //}
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

        // Restore the original console colors
        Console.BackgroundColor = originalBackgroundColor;
        Console.ForegroundColor = originalForegroundColor;
    }

    /*
    public void UptadeSpecificCellsAfterAnimalMove(int oldRow, int oldCol, int newRow, int newCol)
    {
        // Calculate the console position based on the row and col.
        // Adjust these values based on the layout of your zoo map in the console (the zoo.CourserTop and zoo.CourserLeft are the top left corner of the zoo map)
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = ((oldRow + i) % _zooMap.Length) * 2;
                int consoleCol = ((oldCol + j) % _zooMap[0].Length) * 2;
                if (consoleRow >= 0 && consoleRow < Console.WindowHeight && consoleCol >= 0 && consoleCol < Console.WindowWidth)
                {
                    Console.SetCursorPosition(consoleCol + CoursorLeft, consoleRow + CoursorTop);
                    Console.Write(' ');
                }

                //Console.SetCursorPosition(consoleCol, consoleRow);
                //Console.Write(' ');
            }
        }




        // Set text and background color based on the animal type
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = ((newRow + i) % _zooMap.Length) * 2;
                int consoleCol = ((newCol + j) % _zooMap[0].Length) * 2;
                if (consoleRow >= 0 && consoleRow < Console.WindowHeight && consoleCol >= 0 && consoleCol < Console.WindowWidth)
                {
                    Console.SetCursorPosition(consoleCol + CoursorLeft, consoleRow + CoursorTop);
                    Console.Write(' ');
                }

                //Console.SetCursorPosition(consoleCol, consoleRow);
                //Console.Write(' ');

                char animalChar = _zooMap[newRow][newCol];
                switch (animalChar)
                {
                    case 'L':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 'M':
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        animalChar = '.'; // Dot for empty space
                        break;
                }

                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.Write(animalChar);
                Console.ResetColor(); // Reset to default colors after each character
            }
        }


        
    }*/

    public void UpdateSpecificCellsAfterAnimalMove2(int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        ClearSpecificCells2(oldRow, oldCol);

        // Update the new position
        UpdateSpecificCells2(newRow, newCol);
    }

    public void ClearSpecificCells2(int row, int col)
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
    
    public void UpdateSpecificCells2(int row, int col)
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
    

    public void UpdateSpecificCellsAfterAnimalMove(int oldRow, int oldCol, int newRow, int newCol)
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

        // Determine the colors based on the animal type
        switch (animalChar)
        {
            case 'L':
                foregroundColor = ConsoleColor.Red;
                backgroundColor = ConsoleColor.Yellow;
                break;
            case 'M':
                foregroundColor = ConsoleColor.Green;
                backgroundColor = ConsoleColor.DarkGreen;
                break;
            default:
                foregroundColor = ConsoleColor.Gray;
                backgroundColor = ConsoleColor.Black;
                animalChar = '.'; // Dot for empty space
                break;
        }

        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                int consoleRow = CoursorTop + (row + i) * 2;
                int consoleCol = CoursorLeft + (col + j) * 4; // Each cell takes up 4 characters in width
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
        }
    }




    //public void InitializePreviousZooMap(int size)
    //{
    //    _previousZooMap = new char[size][];
    //    for (int i = 0; i < size; i++)
    //    {
    //        _previousZooMap[i] = new char[size];
    //        Array.Fill(_previousZooMap[i], ' '); // Initialize with spaces.
    //    }
    //}

    /*
    public void UpdateChangedCellsOnly()
    {
        // Assuming _zooMap has already been updated with the new state.
        for (int row = 0; row < _zooMap.Length; row++)
        {
            for (int col = 0; col < _zooMap[row].Length; col++)
            {
                if (_zooMap[row][col] != _previousZooMap[row][col])
                {
                    // Calculate the console position based on the row and col.
                    // Adjust these values based on how your zoo map is positioned in the console.
                    int consoleRow = row + 2; // +2 for example, adjust based on your console layout
                    int consoleCol = (col * 2) + 4; // *2 and +4 for example, adjust for your layout

                    Console.SetCursorPosition(consoleCol, consoleRow);

                    // Set text and background color based on the animal type
                    char animalChar = _zooMap[row][col];
                    switch (animalChar)
                    {
                        case 'L':
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        case 'M':
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            animalChar = '.'; // Dot for empty space
                            break;
                    }

                    Console.Write(animalChar);
                    Console.ResetColor(); // Reset to default colors after each character

                    // Update the previous map to match the new state.
                    _previousZooMap[row][col] = _zooMap[row][col];
                }
            }
        }
    }*/


    // Version 1 of PlotZoo, before adding colors and grid lines
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


    // Version 2 of PlotZoo, with colors and grid lines, but without background colors
    /*
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
    }*/


    // Old version of MoveAllAnimals
    //public void PlaceAnimal(IAnimal animal)
    //{
    //    Random rnd = new Random();
    //    bool placed = false;
    //    while (!placed)
    //    {
    //        int row = rnd.Next(_zooMap.Length - AnimalMatrixSize + 1);
    //        int col = rnd.Next(_zooMap[0].Length - AnimalMatrixSize + 1);

    //        // Check if the 2x2 area is empty
    //        bool areaIsEmpty = FindEmptySpace(row, col);

    //        if (areaIsEmpty)
    //        {
    //            InsertAnimal(animal, row, col);
    //            placed = true;
    //        }
    //    }
    //}

    //public void InsertAnimal(IAnimal animal, int row, int col)
    //{
    //    for (int i = 0; i < AnimalMatrixSize; i++)
    //    {
    //        for (int j = 0; j < AnimalMatrixSize; j++)
    //        {
    //            _zooMap[row + i][col + j] = animal.getName()[0]; // Use the first letter of the animal's name
    //        }
    //    }
    //    _animalPositions[animal] = (row, col); // Track the top-left position of the animal's matrix
    //}

    //public bool FindEmptySpace(int row, int col)
    //{
    //    bool areaIsEmpty = true;
    //    for (int i = 0; i < AnimalMatrixSize; i++)
    //    {
    //        for (int j = 0; j < AnimalMatrixSize; j++)
    //        {
    //            if (_zooMap[row + i][col + j] != ' ')
    //            {
    //                areaIsEmpty = false;
    //                break;
    //            }
    //        }
    //        if (!areaIsEmpty) break;
    //    }
    //    return areaIsEmpty;
    //}

    /////////////////////////////////////// Helper Functions ///////////////////////////////////////
    public ConsoleColor GetAnimalForegroundColor(char animalChar)
    {
        if (_animalTypeMap.ContainsKey(animalChar))
        {
            return _animalTypeMap[animalChar].AnimalForegroundColor;
        }
        return ConsoleColor.Gray;
    }

    public ConsoleColor GetAnimalBackgroundColor(char animalChar)
    {
        if (_animalTypeMap.ContainsKey(animalChar))
        {
            return _animalTypeMap[animalChar].AnimalBackgroundColor;
        }
        return ConsoleColor.Black;
    }

    public char GetMappedAnimalChar(char animalChar)
    {
        if (_animalTypeMap.ContainsKey(animalChar))
        {
            return animalChar;
        }
        return '.';
    }
}