using System;


class Program
{
    static void Main(string[] args)
    {
        try
        {
            ZooManager zooManager = new ZooManager();
            zooManager.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}