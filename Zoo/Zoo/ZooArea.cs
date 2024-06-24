using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ZooArea
{
    protected Zoo _zoo { get; }
    public Animal[][] _zooMap { get; private set; }
    private int[] zooRow;
    private int[] zooCol;
    public int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space

    protected readonly GPSTracker _gpsTracker;


    public ZooArea(Zoo zoo, int size, GPSTracker gpsTracker)
    {
        _zoo = zoo;
        _gpsTracker = gpsTracker;
        SetZooSize(size);
    }


    public void SetZooSize(int size)
    {
        _zooMap = new Animal[size][];

        for (int i = 0; i < size; i++)
        {
            _zooMap[i] = new Animal[size];
            Array.Fill(_zooMap[i], null); // fill the array with spaces indicating empty spaces.
        }

        zooCol = new int[size];
        Array.Fill(zooCol, 0);
        zooRow = new int[size];
        Array.Fill(zooRow, 0);
    }


    public virtual bool PlaceAnimal(Animal animal)
    {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
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
                            _zooMap[r + i][c + j] = animal;
                        }
                    }
                    _gpsTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(r,c));

                    for (int i = 0; i < AnimalMatrixSize; i++)
                    {
                        zooRow[r + i] += AnimalMatrixSize;
                        zooCol[c + i] += AnimalMatrixSize;
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
            currentRow >= _zooMap.Length || currentCol >= _zooMap[0].Length || newRow >= _zooMap.Length || newCol >= _zooMap[0].Length)
        {
            return;
        }
        zooCol[currentCol] -= AnimalMatrixSize;
        zooCol[newCol] += AnimalMatrixSize;
        zooRow[currentRow] -= AnimalMatrixSize;
        zooRow[newRow] += AnimalMatrixSize;
    }


    ///////////////////////////////////////////////////////////////////////////////////
    /////////////////////// MoveAllAnimals Helper Functions //////////////////////////
    public virtual bool CheckIfEmpty(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                if (_zooMap[row + i][col + j] != null)
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
                _zooMap[row + i][col + j] = null;
            }
        }
    }


    public virtual void InsertAnimal(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                _zooMap[row + i][col + j] = animal;
            }
        }
        _gpsTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(row, col)); // track the top-left position of the animal's matrix
    }


    public virtual void UpdateSpecificCellsAfterAnimalMove(Animal animal, ZooArea zooArea, int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        _zoo._zooPlot.ClearSpecificCells(animal, zooArea, oldRow, oldCol);

        // Update the new position
        _zoo._zooPlot.UpdateSpecificCells(animal, zooArea, newRow, newCol);

        //Console.SetCursorPosition(this.lastCourserPosition.col, this.lastCourserPosition.row);
    }
}