using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ZooArea {
    private Zoo _zoo;
    private readonly GPSTracker _gpsTracker;
    public Animal[][] _zooMap { get; private set; }
    const int AnimalMatrixSize = 2; // Each animal occupies a 2x2 space

    public ZooArea(Zoo zoo, int size, GPSTracker gpsTracker) {
        _zoo = zoo;
        _gpsTracker = gpsTracker;
        SetZooSize(size);
    }


    public void SetZooSize(int size) {
        _zooMap = new Animal[size][];
        //CourserPosition[][] zooCourserPositions = new CourserPosition[size][];

        for (int i = 0; i < size; i++) {
            _zooMap[i] = new Animal[size];
            Array.Fill(_zooMap[i], null); // fill the array with spaces indicating empty spaces.

            //zooCourserPositions[i] = new CourserPosition[size]; // Initialize the inner array
            //Array.Fill(zooCourserPositions[i], new CourserPosition { row = 0, col = 0 });
        }
    }


    public virtual bool PlaceAnimal(Animal animal) {
        int rowLength = _zooMap.Length;
        int colLength = _zooMap[0].Length;
        for (int r = 0; r < rowLength - 1; r++) {
            for (int c = 0; c < colLength - 1; c++) {
                if (CheckIfEmpty(r, c)) {
                    for (int i = 0; i < AnimalMatrixSize; i++) {
                        for (int j = 0; j < AnimalMatrixSize; j++) {
                            _zooMap[r + i][c + j] = animal;
                        }
                    }

                    //animal.AnimalPosition = (r, c);
                    _gpsTracker.AddOrUpdatePosition(animal.AnimalId, new Point(r, c));
                    // GPSTracker.AddOrUpdateZoo(animal.AnimalId, this._zoo);

                    return true;
                }
            }
        }

        return false;
    }


    ///////////////////////////////////////////////////////////////////////////////////
    /////////////////////// MoveAllAnimals Helper Functions //////////////////////////
    public bool CheckIfEmpty(int row, int col) {
        for (int i = 0; i < AnimalMatrixSize; i++) {
            for (int j = 0; j < AnimalMatrixSize; j++) {
                if (_zooMap[row + i][col + j] != null) {
                    return false;
                }
            }
        }

        return true;
    }

    public void ClearAnimalPosition(int row, int col) {
        for (int i = 0; i < AnimalMatrixSize; i++) {
            for (int j = 0; j < AnimalMatrixSize; j++) {
                _zooMap[row + i][col + j] = null;
            }
        }
    }

    public void InsertAnimal(Animal animal, int row, int col) {
        for (int i = 0; i < AnimalMatrixSize; i++) {
            for (int j = 0; j < AnimalMatrixSize; j++) {
                _zooMap[row + i][col + j] = animal;
            }
        }

        //animal.AnimalPosition = (row, col); 
        _gpsTracker.AddOrUpdatePosition(animal.AnimalId,
            new Point(row, col)); // track the top-left position of the animal's matrix
    }
}