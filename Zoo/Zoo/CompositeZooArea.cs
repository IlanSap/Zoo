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

    public Dictionary<AnimalType, ZooArea> Areas= new Dictionary<AnimalType, ZooArea>();
    public Dictionary<ZooArea, int> AreaStartRow = new Dictionary<ZooArea, int>();


    public CompositeZooArea(Zoo zoo, int size, GPSTracker gpsTracker, ZooPlot zooPlot) : base(zoo, size, gpsTracker) => this._zooPlot = zooPlot;


    public void PlotAreas()
    {
        int marginSize = 5;
        int startRow = 0;

        foreach (var area in Areas.Values)
        {
            _zooPlot.PlotZoo(area, startRow);
            AreaStartRow[area] = startRow;
            startRow += (int)(area.ZooMap.Length + 3 + marginSize);
        }
    }


    public override bool PlaceAnimal(Animal animal)
    {
        ZooArea area;

        if (! Areas.TryGetValue(animal.AnimalType, out ZooArea? value))
        {
            area = new ZooArea(this.Zoo, _size, GpsTracker); // Initialize with the same size and tracker
            Areas[animal.AnimalType] = area;
        }
        else
        {
            area = value;
        }

        return area.PlaceAnimal(animal);
    }


    public override void ClearAnimalPosition(Animal animal, int row, int col)
    {
        Areas[animal.AnimalType].ClearAnimalPosition(animal, row, col);
    }


    public override void InsertAnimal(Animal animal, int row, int col)
    {
        if (Areas.TryGetValue(animal.AnimalType, out ZooArea? value))
        {
            value.InsertAnimal(animal, row, col);
        }
    }


    public override bool CheckIfEmpty(Animal animal, int row, int col)
    {
        return Areas[animal.AnimalType].CheckIfEmpty(animal, row, col);
    }


    public override void UpdateRowAndColArrays(Animal animal, int currentRow, int currentCol, int newRow, int newCol)
    {
        Areas[animal.AnimalType].UpdateRowAndColArrays(animal, currentRow, currentCol, newRow, newCol);
    }


    public override void UpdateSpecificCellsAfterAnimalMove(Animal animal, ZooArea zooArea, int oldRow, int oldCol, int newRow, int newCol)
    {
        // Clear the old position
        int row= oldRow + AreaStartRow[Areas[animal.AnimalType]];
        Zoo.ZooPlot.ClearSpecificCells(animal, Areas[animal.AnimalType], row, oldCol);

        // Update the new position
        row= newRow + AreaStartRow[Areas[animal.AnimalType]];
        Zoo.ZooPlot.UpdateSpecificCells(animal, Areas[animal.AnimalType], row, newCol);

        //Console.SetCursorPosition(this.lastCourserPosition.Col, this.lastCourserPosition.Row);
    }
}
