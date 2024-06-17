using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ZooArea
{
    private Zoo _zoo;
    public Animal[][] _zooMap { get; private set; }
    private int[] zooRow;
    private int[] zooCol;
    const int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space

    public ZooArea(Zoo zoo, int size)
    {
        _zoo = zoo;
        SetZooSize(size);
    }


    public void SetZooSize(int size)
    {
        _zooMap = new Animal[size][];
        CourserPosition[][] zooCourserPositions = new CourserPosition[size][];

        for (int i = 0; i < size; i++)
        {
            _zooMap[i] = new Animal[size];
            Array.Fill(_zooMap[i], null); // fill the array with spaces indicating empty spaces.

            zooCourserPositions[i] = new CourserPosition[size]; // Initialize the inner array
            Array.Fill(zooCourserPositions[i], new CourserPosition { row = 0, col = 0 });

        }
        zooCol = new int[size];
        Array.Fill(zooCol, 0);
        zooRow = new int[size];
        Array.Fill(zooRow, 0);
        _zoo._zooPlot._zooCourserPositions = zooCourserPositions;
    }


    public bool PlaceAnimal(Animal animal)
    {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++)
        {
            for (int c = 0; c < colLength - 1; c++)
            {
                if (CheckIfEmpty(r, c))
                {
                    for (int i = 0; i < AnimalMatrixSize; i++)
                    {
                        for (int j = 0; j < AnimalMatrixSize; j++)
                        {
                            _zooMap[r + i][c + j] = animal;
                        }
                    }
                    //animal.AnimalPosition = (r, c);
                    GPSTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(r,c));
                    // GPSTracker.AddOrUpdateZoo(animal.AnimalId, this._zoo);
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


    public void UpdateRowAndColArrays(int currentRow, int currentCol, int newRow, int newCol)
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
    public bool CheckIfEmpty(int row, int col)
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

    public void ClearAnimalPosition(int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                _zooMap[row + i][col + j] = null;
            }
        }
    }

    public void InsertAnimal(Animal animal, int row, int col)
    {
        for (int i = 0; i < AnimalMatrixSize; i++)
        {
            for (int j = 0; j < AnimalMatrixSize; j++)
            {
                _zooMap[row + i][col + j] = animal;
            }
        }
        //animal.AnimalPosition = (row, col); 
        GPSTracker.AddOrUpdatePosition(animal.AnimalId, new AnimalPosition(row, col)); // track the top-left position of the animal's matrix
    }
}