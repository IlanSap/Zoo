namespace Zoo.Zoo;

public class CompositeZooArea:ZooArea {

    private Dictionary<AnimalType, ZooArea> _areas = new Dictionary<AnimalType, ZooArea>();
    public CompositeZooArea(Zoo zoo, int size, GPSTracker gpsTracker) : base(zoo, size, gpsTracker) {
    }

    public override bool PlaceAnimal(Animal animal) {
        ZooArea area;

        if (!_areas.TryGetValue(animal.AnimalType, out area)) {
            area = new ZooArea();
            _areas[animal.AnimalType] = area;
        }

        area.PlaceAnimal(animal);
    }
}
