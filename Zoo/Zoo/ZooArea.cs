using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooProject.Animals.AnimalTypes;

namespace ZooProject.Zoo;
public class ZooArea
{
    protected Zoo Zoo { get; }
    public Animal[][] ZooMap { get; private set; }
    private int[] _zooRow;
    private int[] _zooCol;
    public int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space

    protected readonly GPSTracker GpsTracker;


    public ZooArea(Zoo zoo, int size, GPSTracker gpsTracker)
    {
        Zoo = zoo;
        GpsTracker = gpsTracker;
        SetZooSize(size);
    }


    public void SetZooSize(int size)
    {
        ZooMap = new Animal[size][];

        for (int i = 0; i < size; i++)
        {
            ZooMap[i] = new Animal[size];
            Array.Fill(ZooMap[i], null); // fill the array with spaces indicating empty spaces.
        }

        _zooCol = new int[size];
        Array.Fill(_zooCol, 0);
        _zooRow = new int[size];
        Array.Fill(_zooRow, 0);
    }


    public virtual bool PlaceAnimal(Animal animal)
    {
        int rowLength = ZooMap.Length;
        int colLength = ZooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++)
        {
            for (int c = 0; c < colLength - 1; c++)
            {
                if (CheckIfEmpty(animal, r, c))
                {
                    for (int i = 0; i < AnimalMatrixSize; i++)
                    {
                        for (int j = 0; j < AnimalMatrixSize; j++)
                        {
                            ZooMap[r + i][c + j] = animal;
                        }
                    }
                    GpsTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(r,c));

                    for (int i = 0; i < AnimalMatrixSize; i++)
                    {
                        _zooRow[r + i] += AnimalMatrixSize;
                        _zooCol[c + i] += AnimalMatrixSize;
                    }
                    return true;
                }
            }
        }
        return false;
    }


    public virtual void UpdateRowAndColArrays(Animal animal, int currentRow, int currentCol, int newRow, int newCol)
    {
        if (currentRow == newRow && currentCol == newCol)
        {
            return;
        }
        if (currentRow < 0 || currentCol < 0 || newRow < 0 || newCol < 0 ||
            currentRow >= ZooMap.Length || currentCol >= ZooMap[0].Length || newRow >= ZooMap.Length || newCol >= ZooMap[0].Length)
        {
            return;
        }
        _zooCol[currentCol] -= AnimalMatrixSize;
        _zooCol[newCol] += AnimalMatrixSize;
        _zooRow[currentRow] -= AnimalMatrixSize;
        _zooRow[newRow] += AnimalMatrixSize;
    }


    ///////////////////////////////////////////////////////////////////////////////////
    /////////////////////// MoveAllAnimals Helper Functions //////////////////////////
    public virtual bool CheckIfEmpty(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                if (ZooMap[row + i][col + j] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }


    public virtual void ClearAnimalPosition(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                ZooMap[row + i][col + j] = null;
            }
        }
    }


    public virtual void InsertAnimal(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                ZooMap[row + i][col + j] = animal;
            }
        }
        GpsTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(row, col)); // track the top-left position of the animal's matrix
    }


    public virtual void UpdateSpecificCellsAfterAnimalMove(Animal animal, ZooArea zooArea, int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        Zoo.ZooPlot.ClearSpecificCells(animal, zooArea, oldRow, oldCol);

        // Update the new position
        Zoo.ZooPlot.UpdateSpecificCells(animal, zooArea, newRow, newCol);

        //Console.SetCursorPosition(this.lastCourserPosition.Col, this.lastCourserPosition.Row);
    }
}