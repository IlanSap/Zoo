using System;

public class ConsoleHelper
{
    public int GetZooCount()
    {

        Console.WriteLine("Enter Number of Zoos:");
        if (!int.TryParse(Console.ReadLine(), out int numberOfZoos) || numberOfZoos <= 0)
        {
            throw new Exception("Invalid input for Zoo Size. Please enter a positive integer.");
        }

        return numberOfZoos;
    }


    public int GetZooSize()
    {
        Console.WriteLine("Enter Zoo Size:");
        /*if (zooManager._zooList.Count > 0)
            zooManager._zooList[zooManager._zooList.Count - 1]._zooPlot.lastCourserPosition.row++;*/

        if (!int.TryParse(Console.ReadLine(), out int zooSize) || zooSize <= 0)
        {
            throw new Exception("Invalid input for Zoo Size. Please enter a positive integer.");
        }

        return zooSize;
    }


    public int GetAnimalCount()
    {
        Console.WriteLine("Enter number of animals to place in the zoo:");
        if (!int.TryParse(Console.ReadLine(), out int animalCount) || animalCount <= 0)
        {
            throw new Exception("Invalid input for number of animals. Please enter a positive integer.");
        }
        return animalCount;
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


    public int GetRunType()
    {
        Console.WriteLine("Enter 1 for regular run, 2 for composite run:");
        if (!int.TryParse(Console.ReadLine(), out int runType) || (runType != 1 && runType != 2))
        {
            Console.WriteLine("Invalid input for run type. Please enter 1 or 2.");
            return -1;
        }
        return runType;
    }
}   