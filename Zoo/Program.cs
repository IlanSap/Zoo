using System;
using System.Collections.Generic;
using static Zoo;

// TO_DO: Maybe- allow user to add new animal types to the zoo

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Zoo Size:");
        if (!int.TryParse(Console.ReadLine(), out int zooSize) || zooSize <= 0)
        {
            Console.WriteLine("Invalid input for Zoo Size. Please enter a positive integer.");
            return;
        }

        Zoo zoo = new Zoo(); // Directly instantiate the Zoo class
        zoo.SetZooSize(zooSize);

        System.Threading.Timer moveAnimalsTimer = null; // Declare the timer but don't initialize it yet

        while (true)
        {
            Console.WriteLine("Choose Option: \n1. Add Lion \n2. Add Monkey \n3. Move All Animals  \n4. Generate Animals \n5. Start Moving Animals Timer \n6. EXIT");
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
                continue;
            }

            switch (option)
            {
                case 1:
                    IAnimalFactory lionFactory = new LionFactory();
                    IAnimal lion = lionFactory.CreateAnimal("Simba");
                    zoo.AddAnimal(lion);
                    zoo.PlaceAnimal2(lion);
                    zoo.PlotZoo();
                    continue;
                case 2:
                    IAnimalFactory monkeyFactory = new MonkeyFactory();
                    IAnimal monkey = monkeyFactory.CreateAnimal("George");
                    zoo.AddAnimal(monkey);
                    zoo.PlaceAnimal2(monkey);
                    zoo.PlotZoo();
                    continue;
                case 3:
                    zoo.MoveAllAnimals();
                    zoo.PlotZoo();
                    continue;
                case 4:
                    Console.WriteLine("Enter animal type (Lion/Monkey):");
                    string animalType = Console.ReadLine();
                    Console.WriteLine("Enter number of animals to generate:");
                    if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                    {
                        Console.WriteLine("Invalid input for number of animals. Please enter a positive integer.");
                        continue;
                    }
                    zoo.GenerateAnimals(animalType, count);
                    zoo.PlotZoo();
                    continue;
                case 5:
                    if (moveAnimalsTimer == null)
                    {
                        Console.WriteLine("Enter the interval for moving animals (in seconds):");
                        if (!int.TryParse(Console.ReadLine(), out int intervalSeconds) || intervalSeconds <= 0)
                        {
                            Console.WriteLine("Invalid input for interval. Please enter a positive integer.");
                            continue;
                        }
                        Console.Clear(); // Clear the console to remove the previous zoo map
                        zoo.PlotZoo();
                        moveAnimalsTimer = InitializeTimer(zoo, intervalSeconds);
                    }
                    else
                    {
                        Console.WriteLine("Timer is already running.");
                    }
                    continue;
                case 6:
                    moveAnimalsTimer?.Dispose();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    continue;
            }
        }
    }

    static System.Threading.Timer InitializeTimer(Zoo zoo, int intervalSeconds)
    {
        return new System.Threading.Timer(
            //callback: _ => MoveAnimalsEvent(zoo),
            callback: _ => MoveAnimalsEvent(zoo),
            state: null,
            dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
            period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
        );
    }

    static void MoveAnimalsEvent(Zoo zoo)
    {
        //Console.Clear(); // Clear the console to remove the previous zoo map
        zoo.MoveAllAnimals();
        //zoo.PlotZoo();
        //Console.WriteLine("Animals moved automatically.");
    }
}


// Old Version of Program.cs, using SingletonZoo instead of Zoo
/*
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Zoo Size:");
        int zooSize = Convert.ToInt32(Console.ReadLine());
        SingletonZoo singletonZoo = SingletonZoo.Instance;
        singletonZoo.SetZooSize(zooSize);

        System.Threading.Timer moveAnimalsTimer = null; // Declare the timer but don't initialize it yet

        while (true)
        {
            Console.WriteLine("Choose Option: \n1. Add Lion \n2. Add Monkey \n3. Move All Animals  \n4. Generate Animals \n5. Start Moving Animals Timer \n6. EXIT");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    IAnimalFactory lionFactory = new LionFactory();
                    IAnimal lion = lionFactory.CreateAnimal("Simba");
                    singletonZoo.AddAnimal(lion);
                    //singletonZoo.PlaceAnimal(lion);
                    singletonZoo.PlaceAnimal2(lion);
                    singletonZoo.PlotZoo();
                    continue;
                case 2:
                    IAnimalFactory monkeyFactory = new MonkeyFactory();
                    IAnimal monkey = monkeyFactory.CreateAnimal("George");
                    singletonZoo.AddAnimal(monkey);
                    //singletonZoo.PlaceAnimal(monkey);
                    singletonZoo.PlaceAnimal2(monkey);
                    singletonZoo.PlotZoo();
                    continue;
                case 3:
                    singletonZoo.MoveAllAnimals();
                    singletonZoo.PlotZoo();
                    continue;
                case 4:
                    Console.WriteLine("Enter animal type (Lion/Monkey):");
                    string animalType = Console.ReadLine();
                    Console.WriteLine("Enter number of animals to generate:");
                    int count = Convert.ToInt32(Console.ReadLine());
                    singletonZoo.GenerateAnimals(animalType, count);
                    singletonZoo.PlotZoo();
                    continue;
                case 5:
                    if (moveAnimalsTimer == null)
                    {
                        Console.WriteLine("Enter the interval for moving animals (in seconds):");
                        int intervalSeconds = Convert.ToInt32(Console.ReadLine());
                        moveAnimalsTimer = InitializeTimer(singletonZoo, intervalSeconds);
                    }
                    else
                    {
                        Console.WriteLine("Timer is already running.");
                    }
                    continue;
                case 6:
                    moveAnimalsTimer?.Dispose();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    continue;
            }
        }
    }

    static System.Threading.Timer InitializeTimer(SingletonZoo zoo, int intervalSeconds)
    {
        return new System.Threading.Timer(
            callback: _ => MoveAnimalsEvent(zoo),
            state: null,
            dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
            period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
        );
    }

    static void MoveAnimalsEvent(SingletonZoo zoo)
    {
        Console.Clear(); // Clear the console to remove the previous zoo map
        zoo.MoveAllAnimals();
        zoo.PlotZoo();
        Console.WriteLine("Animals moved automatically.");
    }
}
*/


/*
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Zoo Size:");
        int zooSize = Convert.ToInt32(Console.ReadLine());
        Zoo zoo = new Zoo(); // Directly instantiate the Zoo class
        zoo.SetZooSize(zooSize);

        System.Threading.Timer moveAnimalsTimer = null; // Declare the timer but don't initialize it yet

        while (true)
        {
            Console.WriteLine("Choose Option: \n1. Add Lion \n2. Add Monkey \n3. Move All Animals  \n4. Generate Animals \n5. Start Moving Animals Timer \n6. EXIT");
            int option = Convert.ToInt32(Console.ReadLine());


            switch (option)
            {
                case 1:
                    IAnimalFactory lionFactory = new LionFactory();
                    IAnimal lion = lionFactory.CreateAnimal("Simba");
                    zoo.AddAnimal(lion);
                    //zoo.PlaceAnimal(lion);
                    zoo.PlaceAnimal2(lion);
                    zoo.PlotZoo();
                    continue;
                case 2:
                    IAnimalFactory monkeyFactory = new MonkeyFactory();
                    IAnimal monkey = monkeyFactory.CreateAnimal("George");
                    zoo.AddAnimal(monkey);
                    //zoo.PlaceAnimal(monkey);
                    zoo.PlaceAnimal2(monkey);
                    zoo.PlotZoo();
                    continue;
                case 3:
                    zoo.MoveAllAnimals();
                    zoo.PlotZoo();
                    //zoo.PlotZoo2();
                    continue;
                case 4:
                    Console.WriteLine("Enter animal type (Lion/Monkey):");
                    string animalType = Console.ReadLine();
                    Console.WriteLine("Enter number of animals to generate:");
                    int count = Convert.ToInt32(Console.ReadLine());
                    zoo.GenerateAnimals(animalType, count);
                    zoo.PlotZoo();
                    //zoo.PlotZoo2();
                    

                    //ZooPlotForm zooPlotForm = new ZooPlotForm(zoo.getZooMap());
                    //zooPlotForm.Show();
                    continue;
                case 5:
                    if (moveAnimalsTimer == null)
                    {
                        Console.WriteLine("Enter the interval for moving animals (in seconds):");
                        int intervalSeconds = Convert.ToInt32(Console.ReadLine());
                        moveAnimalsTimer = InitializeTimer(zoo, intervalSeconds);
                    }
                    else
                    {
                        Console.WriteLine("Timer is already running.");
                    }
                    continue;
                case 6:
                    moveAnimalsTimer?.Dispose();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    continue;
            }
        }
    }

    static System.Threading.Timer InitializeTimer(Zoo zoo, int intervalSeconds)
    {
        return new System.Threading.Timer(
            callback: _ => MoveAnimalsEvent(zoo),
            state: null,
            dueTime: TimeSpan.FromSeconds(intervalSeconds), // Time to wait before the first execution
            period: TimeSpan.FromSeconds(intervalSeconds) // Time to wait between executions
        );
    }

    static void MoveAnimalsEvent(Zoo zoo)
    {
        Console.Clear(); // Clear the console to remove the previous zoo map
        zoo.MoveAllAnimals();
        zoo.PlotZoo();
        //zoo.PlotZoo2();
        Console.WriteLine("Animals moved automatically.");
    }
}
*/

