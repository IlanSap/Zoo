using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ZooPlot
{
    private static ConsoleColor deafultForegroundColor= ConsoleColor.Gray;
    private static ConsoleColor deafultBackgroundColor = ConsoleColor.Black;

    public static void PlotZoo(Zoo zoo)
    {
        // Save the original console colors to restore them later
        var originalBackgroundColor = Console.BackgroundColor;
        var originalForegroundColor = Console.ForegroundColor;

        // Print column indicators with improved spacing
        Console.Write("    "); // Space for row indicators
        for (int col = 0; col < zoo._zooArea._zooMap[0].Length; col++)
        {
            Console.Write($"{col % 100,2} "); // Use modulo for double-digit numbers, with padding for alignment
        }
        Console.WriteLine();

        // Print top border with improved visibility
        Console.Write("   +"); // Align with row indicators
        for (int col = 0; col < zoo._zooArea._zooMap[0].Length; col++)
        {
            Console.Write("---");
        }
        Console.WriteLine("+");

        for (int i = 0; i < zoo._zooArea._zooMap.Length; i++)
        {
            // Print row indicator with padding for alignment
            Console.Write($"{i % 100,3} |"); // Use modulo for double-digit numbers

            for (int j = 0; j < zoo._zooArea._zooMap[i].Length; j++)
            {
                zoo._zooCourserPositions[i][j] = new CourserPosition { row = Console.CursorTop, col = Console.CursorLeft };
                // Set text and background color based on the animal type
                Animal animal = zoo._zooArea._zooMap[i][j];
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
        for (int col = 0; col < zoo._zooArea._zooMap[0].Length; col++)
        {
            Console.Write("---");
        }
        Console.WriteLine("+");

        PrintLegend(zoo);

        zoo.lastCourserPosition = new CourserPosition { row = Console.CursorTop, col = Console.CursorLeft };

        // Restore the original console colors
        Console.BackgroundColor = originalBackgroundColor;
        Console.ForegroundColor = originalForegroundColor;
    }

    private static void PrintLegend(Zoo zoo)
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
            Console.WriteLine($"{animal.AnimalType.ToString()[0]} - {animal.AnimalType}");
        }
        Console.ResetColor();
        Console.WriteLine(" . - Empty space");
    }

    public static void UpdateSpecificCellsAfterAnimalMove(Zoo zoo, int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        ClearSpecificCells(zoo, oldRow, oldCol);

        // Update the new position
        UpdateSpecificCells(zoo, newRow, newCol);

        Console.SetCursorPosition(zoo.lastCourserPosition.col, zoo.lastCourserPosition.row);
    }

    private static void ClearSpecificCells(Zoo zoo, int row, int col)
    {
        for (int i = 0; i < zoo.AnimalMatrixSize; i++)
        {
            for (int j = 0; j < zoo.AnimalMatrixSize; j++)
            {
                int consoleRow = zoo._zooCourserPositions[row + i][col + j].row;
                int consoleCol = zoo._zooCourserPositions[row + i][col + j].col;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = deafultForegroundColor;
                Console.BackgroundColor = deafultBackgroundColor;
                Console.Write(" . "); // Clear the 2x2 space
            }
        }
    }

    private static void UpdateSpecificCells(Zoo zoo, int row, int col)
    {
        Animal animal = zoo._zooArea._zooMap[row][col];
        char animalChar = GetMappedAnimalChar(animal);
        ConsoleColor foregroundColor = GetAnimalForegroundColor(animal);
        ConsoleColor backgroundColor = GetAnimalBackgroundColor(animal);

        for (int i = 0; i < zoo.AnimalMatrixSize; i++)
        {
            for (int j = 0; j < zoo.AnimalMatrixSize; j++)
            {
                int consoleRow = zoo._zooCourserPositions[row + i][col + j].row;
                int consoleCol = zoo._zooCourserPositions[row + i][col + j].col;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write($" {animalChar} ");
                Console.ResetColor(); // Reset to default colors after each character
            }
        }
    }

    /////////////////////////////////////// Helper Functions ///////////////////////////////////////

    private static char GetMappedAnimalChar(Animal animal)
    {
        return animal == null ? '.' : animal.AnimalType.ToString()[0];
    }

    private static ConsoleColor GetAnimalForegroundColor(Animal animal) 
    {
        return animal == null ? deafultForegroundColor : animal.AnimalForegroundColor;
    }

    private static ConsoleColor GetAnimalBackgroundColor(Animal animal)
    {
        return animal == null ? deafultBackgroundColor : animal.AnimalBackgroundColor;
    }
}