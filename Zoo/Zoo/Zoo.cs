﻿using System;
using System.Collections.Generic;
using System.Reflection;


public class Zoo
{
    private List<Animal> _animals = new List<Animal>();
    public int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space
    public ZooArea _zooArea;
    public Guid ZooId { get; }= Guid.NewGuid();

    // related to the zoo plot
    public CourserPosition[][] _zooCourserPositions;
    public CourserPosition lastCourserPosition;

    public void SetZooSize(int size)
    {
        _zooArea = new ZooArea(this, size);
    }


    public int GetAnimalMatrixSize()
    {
        return AnimalMatrixSize;
    }


    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }


    public void GenerateAnimals(AnimalType type, int count)
    {
        // IAnimalFactory factory = (IAnimalFactory)Activator.CreateInstance(Type.GetType($"{animalType}Factory"));
        IAnimalFactory factory = new AnimalFactory();
        int numOfSucessfulPlacements = 0;
        for (int i = 0; i < count; i++)
        {
            // IAnimal animal = factory.CreateAnimal($"{animalType} {i + 1}");
            Animal animal = factory.CreateAnimal(type);
            if (_zooArea.PlaceAnimal(animal) == false)
            {
                Console.WriteLine($"The {(animal.AnimalType).ToString()} couldn't be placed.");
            }
            else
            {
                AddAnimal(animal);
                numOfSucessfulPlacements++;
            }
        }
        Console.WriteLine($"{numOfSucessfulPlacements} out of {count} {type.ToString()}s were placed in the zoo.");
    }

    public void MoveAllAnimals()
    {
        foreach (var animal in _animals)
        {
            animal.Move();
        }
    }

    public bool MoveAnimal(Animal animal)
    {
        Random rnd = new Random();
        List <Tuple<int, int>> directions = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 1),  // Right
            new Tuple<int, int>(0, -1), // Left
            new Tuple<int, int>(-1, 0), // Up
            new Tuple<int, int>(1, 0)   // Down
        };
        // Multiply directions by the animal step size
        for (int i = 0; i < directions.Count; i++)
        {
            directions[i] = new Tuple<int, int>(directions[i].Item1 * animal.StepSize, directions[i].Item2 * animal.StepSize);
        }
        var (currentRow, currentCol) = GPSTracker.GetPosition(animal.AnimalId);
        bool moved = false;

        while (directions.Count > 0 && !moved)
        {
            int index = rnd.Next(directions.Count);
            var (dirRow, dirCol) = directions[index];
            directions.RemoveAt(index);

            int newRow = currentRow + dirRow * this.GetAnimalMatrixSize();
            int newCol = currentCol + dirCol * this.GetAnimalMatrixSize();

            if (newRow >= 0 && newRow <= this._zooArea._zooMap.Length - this.GetAnimalMatrixSize() &&
                newCol >= 0 && newCol <= this._zooArea._zooMap[0].Length - this.GetAnimalMatrixSize() &&
                this._zooArea.CheckIfEmpty(newRow, newCol))
            {
                this._zooArea.ClearAnimalPosition(currentRow, currentCol);
                this._zooArea.InsertAnimal(animal, newRow, newCol);
                this._zooArea.UpdateRowAndColArrays(currentRow, currentCol, newRow, newCol);
                ZooPlot.UpdateSpecificCellsAfterAnimalMove(this, currentRow, currentCol, newRow, newCol);
                moved = true;
            }
        }
        if (!moved)
        {
            //Console.WriteLine($"{animal.AnimalType.ToString()} couldn't find a place to move.");
        }
        return moved;
    }


    public bool CanFitAnimals(int animalCount)
    {
        int availableSpace = (_zooArea._zooMap.Length * _zooArea._zooMap[0].Length) / (AnimalMatrixSize * AnimalMatrixSize);
        return animalCount <= availableSpace;
    }
}