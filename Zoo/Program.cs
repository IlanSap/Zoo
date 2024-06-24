using System;


class Program
{
    static void Main(string[] args)
    {
        try
        {
            ZooManager zooManager = new ZooManager();
            ConsoleHelper consoleHelper = new ConsoleHelper();
            if (consoleHelper.GetRunType() == 1)
            {
                zooManager.Run();
            }
            else
            {
                zooManager.RunWithComposite();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}