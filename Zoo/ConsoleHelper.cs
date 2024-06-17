using System;

public class ConsoleHelper
{
    public void GetInputFromUser(ZooManager zooManager)
    {
        try
        {
            Console.WriteLine("Enter Number of Zoos:");
            if (!int.TryParse(Console.ReadLine(), out int numberOfZoos) || numberOfZoos <= 0)
            {
                Console.WriteLine("Invalid input for Zoo Size. Please enter a positive integer.");
                return;
            }
            zooManager.numberOfZoos = numberOfZoos;
            Console.WriteLine();

            for (int i = 0; i < numberOfZoos; i++)
            {
                Console.WriteLine($"Creating Zoo {i + 1}:");
                CreateZoo(zooManager);
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    private void CreateZoo(ZooManager zooManager)
    {
        try
        {
            Console.WriteLine("Enter Zoo Size:");
            /*if (zooManager._zooList.Count > 0)
                zooManager._zooList[zooManager._zooList.Count - 1]._zooPlot.lastCourserPosition.row++;*/

            if (!int.TryParse(Console.ReadLine(), out int zooSize) || zooSize <= 0)
            {
                Console.WriteLine("Invalid input for Zoo Size. Please enter a positive integer.");
                return;
            }

            Console.WriteLine("Enter number of animals to place in the zoo:");
            if (zooManager._zooList.Count > 0)
                zooManager._zooList[zooManager._zooList.Count - 1]._zooPlot.lastCourserPosition.row++;
            //Console.SetCursorPosition(0, (Console.CursorTop + 1));
            if (!int.TryParse(Console.ReadLine(), out int animalCount) || animalCount <= 0)
            {
                Console.WriteLine("Invalid input for number of animals. Please enter a positive integer.");
                return;
            }

            Zoo zoo = zooManager._zooList.Count == 0
                ? new Zoo()
                : new Zoo(zooManager._zooList[zooManager._zooList.Count - 1]._zooPlot.lastCourserPosition);

            //zooManager._zoo = zoo;

            zooManager._zooList.Add(zoo);
            zoo.SetZooSize(zooSize);

            if (zoo.CanFitAnimals(animalCount))
            {
                zooManager.GenerateRandomAnimals(zoo, animalCount);
            }
            else
            {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }

            double intervalSeconds = GetTimeInterval();
            zoo._intervalSeconds = intervalSeconds;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public double GetTimeInterval()
    {
        Console.WriteLine("Enter the interval for moving animals (in seconds):");
        if (!double.TryParse(Console.ReadLine(), out double intervalSeconds) || intervalSeconds <= 0)
        {
            Console.WriteLine("Invalid input for interval. Please enter a positive number.");
            return -1;
        }
        return intervalSeconds;
    }
}   