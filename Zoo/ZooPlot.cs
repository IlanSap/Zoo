using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ZooPlot
{
    private ConsoleColor deafultForegroundColor = ConsoleColor.Gray;
    private ConsoleColor deafultBackgroundColor = ConsoleColor.Black;

    //public CourserPosition[][] _zooCourserPositions;

    public int zooStartRow;
    public int endRow;

    // Convert zoo cell to console cell
    public CourserPosition ConvertZooCellToConsoleCell(int row, int col, int zooStartRow)
    {
        int newRow = zooStartRow + row * 1 + 2;
        int newCol = col * 3 + 5;
        return new CourserPosition { row = newRow, col = newCol };
    }

    public void PlotZoo(ZooArea zooArea, int startRow)
    {
        try
        {
            if (startRow < 0)
            {
                throw new ArgumentOutOfRangeException("startRow", "The zooStartRow parameter must be a non-negative integer.");
            }

            // Print _zooCourserPositions for debugging
            /*Console.WriteLine("Zoo Courser Positions:");
            foreach (var row in _zooCourserPositions)
            {
                foreach (var col in row)
                {
                    Console.Write($"({col.row}, {col.col}) ");
                }
                Console.WriteLine();
            }*/

            int lastRow = 0;


            Console.SetCursorPosition(0, startRow);
            //Console.WriteLine($"Zoo #{zooNumber + 1}:");

            // Save the original console colors to restore them later
            var originalBackgroundColor = Console.BackgroundColor;
            var originalForegroundColor = Console.ForegroundColor;

            // Print column indicators with improved spacing
            Console.Write("    "); // Space for row indicators
            for (int col = 0; col < zooArea._zooMap[0].Length; col++)
            {
                Console.Write($"{col % 100,2} "); // Use modulo for double-digit numbers, with padding for alignment
            }

            Console.WriteLine();

            // Print top border with improved visibility
            Console.Write("   +"); // Align with row indicators
            for (int col = 0; col < zooArea._zooMap[0].Length; col++)
            {
                Console.Write("---");
            }

            Console.WriteLine("+");

            for (int i = 0; i < zooArea._zooMap.Length; i++)
            {
                // Print row indicator with padding for alignment
                Console.Write($"{i % 100,3} |"); // Use modulo for double-digit numbers

                for (int j = 0; j < zooArea._zooMap[i].Length; j++)
                {
/*                    this._zooCourserPositions[i][j] = new CourserPosition
                    { row = Console.CursorTop, col = Console.CursorLeft };*/

                    lastRow = Console.CursorTop;
                    Animal animal = zooArea._zooMap[i][j];
                    char animalChar = GetMappedAnimalChar(animal);
                    Console.ForegroundColor = GetAnimalForegroundColor(animal);
                    Console.BackgroundColor = GetAnimalBackgroundColor(animal);

                    Console.Write($" {animalChar} ");
                    Console.ResetColor(); // Reset to default colors after each character
                }

                Console.WriteLine("|");
            }

            // Print bottom border
            Console.Write("   +"); // Align with row indicators
            for (int col = 0; col < zooArea._zooMap[0].Length; col++)
            {
                Console.Write("---");
            }

            Console.WriteLine("+");

            //PrintLegend(zoo);

            // Print _zooCourserPositions for debugging
            /*Console.WriteLine("Zoo Courser Positions:");
            foreach (var row in _zooCourserPositions)
            {
                foreach (var col in row)
                {
                    Console.Write($"({col.row}, {col.col}) ");
                }
                Console.WriteLine();
            }*/

            //this.lastCourserPosition = new CourserPosition { row = Console.CursorTop, col = Console.CursorTop };

            // Restore the original console colors
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;

            // Used for debugging
/*            // Print the zoo size
            Console.WriteLine("Zoo size: " + zoo._zooArea._zooMap.Length);
            // Print the CourserPosition
            Console.WriteLine("CourserPosition: " + Console.CursorTop + " " + Console.CursorTop);
            // Print the zooStartRow
            Console.WriteLine("StartRow: " + zooStartRow);
            Console.WriteLine("LastRow: " + lastRow);*/
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while plotting the zoo: {ex.Message}");
        }
    }



    public void PrintInstructions()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Press 'q' to quit.");
        Console.SetCursorPosition(0, (Console.CursorTop + 1));
    }

    // Print Legend for every Zoo
    private void PrintLegend(Zoo zoo)
    {
        // Print legend with colors and ASCII characters
        Console.WriteLine("Legend:");

        HashSet<string> animalTypes = new HashSet<string>();
        foreach (var animal in zoo._animals)
        {
            // check if animal is already in the legend
            if (animalTypes.Contains(animal.AnimalType.ToString()))
            {
                continue;
            }
            animalTypes.Add(animal.AnimalType.ToString());
            Console.ForegroundColor = animal.AnimalForegroundColor;
            Console.BackgroundColor = animal.AnimalBackgroundColor;
            Console.WriteLine($"{animal.AnimalType.ToString()[0]} = {animal.AnimalType}");
        }
        Console.ResetColor();
        Console.WriteLine(" . - Empty space");
    }

    // Print Legend for all Zoos
    public void PrintOneLegend(List<Zoo> zoos)
    {
        // Print legend with colors and ASCII characters
        Console.WriteLine("Legend:");

        zoos.SelectMany(z => z._animals)
            .DistinctBy(z => z.AnimalType)
            .ToList()
            .ForEach(a => {
                Console.ForegroundColor = a.AnimalForegroundColor;
                Console.BackgroundColor = a.AnimalBackgroundColor;
                Console.WriteLine($"{a.AnimalType.ToString()[0]} = {a.AnimalType}");
            });

        Console.ResetColor();
        Console.WriteLine(" . - Empty space");
    }


    public void ClearSpecificCells(Animal animal, ZooArea zooArea, int row, int col)
    {
        for (int i = 0; i < zooArea.AnimalMatrixSize; i++)
        {
            for (int j = 0; j < zooArea.AnimalMatrixSize; j++)
            {
                CourserPosition position = ConvertZooCellToConsoleCell(row + i, col + j, this.zooStartRow);
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = deafultForegroundColor;
                Console.BackgroundColor = deafultBackgroundColor;
                Console.Write(" . "); // Clear the 2x2 space
            }
        }
    }


    public void UpdateSpecificCells(Animal animal, ZooArea zooArea, int row, int col)
    {
        char animalChar = GetMappedAnimalChar(animal);
        ConsoleColor foregroundColor = GetAnimalForegroundColor(animal);
        ConsoleColor backgroundColor = GetAnimalBackgroundColor(animal);

        for (int i = 0; i < zooArea.AnimalMatrixSize; i++)
        {
            for (int j = 0; j < zooArea.AnimalMatrixSize; j++)
            {
                CourserPosition position = ConvertZooCellToConsoleCell(row + i, col + j, this.zooStartRow);
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
        }
    }

    /////////////////////////////////////// Helper Functions ///////////////////////////////////////

    private char GetMappedAnimalChar(Animal animal)
    {
        return animal == null ? '.' : animal.AnimalType.ToString()[0];
    }

    private ConsoleColor GetAnimalForegroundColor(Animal animal)
    {
        return animal == null ? deafultForegroundColor : animal.AnimalForegroundColor;
    }

    private ConsoleColor GetAnimalBackgroundColor(Animal animal)
    {
        return animal == null ? deafultBackgroundColor : animal.AnimalBackgroundColor;
    }
}
