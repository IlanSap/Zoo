using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;


public class ZooManager {
    public List<Zoo> _zooList = new List<Zoo>();
    public IAnimalFactory _animalFactory;
    public ConsoleHelper _consoleHelper = new ConsoleHelper();
    


    public ZooManager() {
    }

    public void Run() {
        IAnimalFactory animalFactory = new AnimalFactory();
        this._animalFactory = animalFactory;
        var tracker = new GPSTracker();

        Console.WriteLine();

        int zooCount = _consoleHelper.GetZooCount();
        for (int i = 0; i < zooCount; i++) {
            var zoo = new Zoo(tracker);
            int zooSize = _consoleHelper.GetZooSize();
            zoo.SetZooSize(zooSize);
            zoo._intervalSeconds = _consoleHelper.GetTimeInterval();
            int animalCount = _consoleHelper.GetAnimalCount();


            _zooList.Add(zoo);


            if (zoo.CanFitAnimals(animalCount)) {
                GenerateRandomAnimals(zoo, animalCount);
            } else {
                Console.WriteLine($"The zoo of size {zooSize} cannot fit {animalCount} animals.");
            }

            Console.WriteLine();
        }

        try {
            //Console.WriteLine("Starting the Zoo Management System...");
            Console.Clear();
            Console.SetBufferSize(1000, 1000);

            int marginSize = 5;
            int startRow = 0;
            for (int i = 0; i < _zooList.Count; i++) {
                // Used for debugging
/*                Console.WriteLine($"Creating Zoo {i}:");
                // Print zoo size
                Console.WriteLine($"Zoo Size: {_zooList[i]._zooArea._zooMap.Length}");
                // Print startRow
                Console.WriteLine($"Start Row: {startRow}");*/

                //Console.WriteLine($"Zoo #{i + 1}:");
                _zooList[i]._zooPlot.startRow = startRow;
               
                //InitialZooPlot(_zooList[i]);
                _zooList[i]._zooPlot.PlotZoo(_zooList[i], startRow );
                startRow += (int) (_zooList[i]._zooArea._zooMap.Length + 3 + marginSize);
               _zooList[i].InitializeTimer(_zooList[i]._intervalSeconds);

                if (i == _zooList.Count - 1) {
                    _zooList[i]._zooPlot.PrintOneLegend(_zooList);
                    _zooList[i]._zooPlot.PrintInstructions();
                }
            }

            while (true) {
                if (Console.ReadKey(true).Key == ConsoleKey.Q) {
                    Environment.Exit(0);
                }
            }
        } catch
            (Exception ex) {
            Console.WriteLine($"An error occurred in Run: {ex.Message}");
        }
    }


    public void GenerateRandomAnimals(Zoo zoo, int animalCount) {
        try {
            Array animalTypes = Enum.GetValues(typeof(AnimalType));
            Random random = new Random();
            int remainingAnimals = animalCount;

            while (remainingAnimals > 0) {
                AnimalType animalType = (AnimalType) animalTypes.GetValue(random.Next(animalTypes.Length));
                Animal animal = _animalFactory.CreateAnimal(animalType);
                if (zoo._zooArea.PlaceAnimal(animal) == true) {
                    zoo.AddAnimal(animal);
                    remainingAnimals--;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred while generating random animals: {ex.Message}");
        }
    }


  


    private void InitialZooPlot(Zoo zoo) {
        int margin_size = 5;
        if (zoo == _zooList[0]) {
            //Console.WriteLine("Plotting the first zoo.");
            zoo._zooPlot.PlotZoo(zoo, 2);
        } else {
            int startRow = 0;
            int i = 0;
            for (i = 0; i < _zooList.Count; i++) {
                if (_zooList[i] == zoo)
                    break;
                startRow += (int) (_zooList[i]._zooArea._zooMap.Length + 3 + margin_size);
            }

            //Console.WriteLine($"Plotting zoo at startRow: {startRow}, index: {i}");
            zoo._zooPlot.PlotZoo(zoo, startRow );
        }

        // Used for debugging
        // Print the zoo size
        //Console.WriteLine("Zoo size: " + zoo._zooArea._zooMap.Length);
        // Print the CourserPosition
        //Console.WriteLine("CourserPosition: " + zoo._zooPlot.lastCourserPosition.row + " " + zoo._zooPlot.lastCourserPosition.col);
    }
}