using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooProject.Animals.AnimalTypes;

namespace ZooProject.Zoo;


public class CompositeZooArea : ZooArea
{
    private readonly ZooPlot _zooPlot;
    private readonly int _size = 5;

    public Dictionary<AnimalType, ZooArea> _areas= new Dictionary<AnimalType, ZooArea>();
    public Dictionary<ZooArea, int> _areaStartRow = new Dictionary<ZooArea, int>();


    public CompositeZooArea(Zoo zoo, int size, GPSTracker gpsTracker, ZooPlot zooPlot) : base(zoo, size, gpsTracker) => this._zooPlot = zooPlot;


    public void PlotAreas()
    {
        int marginSize = 5;
        int startRow = 0;

        foreach (var area in _areas.Values)
        {
            _zooPlot.PlotZoo(area, startRow);
            _areaStartRow[area] = startRow;
            startRow += (int)(area._zooMap.Length + 3 + marginSize);
        }
    }


    public override bool PlaceAnimal(Animal animal)
    {
        ZooArea area;

        if (! _areas.TryGetValue(animal.AnimalType, out ZooArea? value))
        {
            area = new ZooArea(this._zoo, _size, _gpsTracker); // Initialize with the same size and tracker
            _areas[animal.AnimalType] = area;
        }
        else
        {
            area = value;
        }

        return area.PlaceAnimal(animal);
    }


    public override void ClearAnimalPosition(Animal animal, int row, int col)
    {
        _areas[animal.AnimalType].ClearAnimalPosition(animal, row, col);
    }


    public override void InsertAnimal(Animal animal, int row, int col)
    {
        if (_areas.TryGetValue(animal.AnimalType, out ZooArea? value))
        {
            value.InsertAnimal(animal, row, col);
        }
    }


    public override bool CheckIfEmpty(Animal animal, int row, int col)
    {
        return _areas[animal.AnimalType].CheckIfEmpty(animal, row, col);
    }


    public override void UpdateRowAndColArrays(Animal animal, int currentRow, int currentCol, int newRow, int newCol)
    {
        _areas[animal.AnimalType].UpdateRowAndColArrays(animal, currentRow, currentCol, newRow, newCol);
    }


    public override void UpdateSpecificCellsAfterAnimalMove(Animal animal, ZooArea zooArea, int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        int row= oldRow + _areaStartRow[_areas[animal.AnimalType]];
        _zoo._zooPlot.ClearSpecificCells(animal, _areas[animal.AnimalType], row, oldCol);

        // Update the new position
        row= newRow + _areaStartRow[_areas[animal.AnimalType]];
        _zoo._zooPlot.UpdateSpecificCells(animal, _areas[animal.AnimalType], row, newCol);

        //Console.SetCursorPosition(this.lastCourserPosition.Col, this.lastCourserPosition.Row);
    }
}
