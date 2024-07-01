using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooProject;

public class GPSTracker
{
    private readonly Dictionary<Guid, AnimalPosition> _animalPositions = new Dictionary<Guid, AnimalPosition>();


    public void AddOrUpdatePosition(Guid animalId, AnimalPosition position)
    {
        if (!_animalPositions.TryAdd(animalId, position))
        {
            _animalPositions[animalId] = position;
        }
    }


    public AnimalPosition GetPosition(Guid animalId)
    {
        return _animalPositions.TryGetValue(animalId, out AnimalPosition value) ? value : new AnimalPosition(-1, -1);
    }


    public void RemovePosition(Guid animalId)
    {
        _animalPositions.Remove(animalId);
    }
}