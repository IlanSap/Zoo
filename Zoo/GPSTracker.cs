using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GPSTracker
{
    private readonly Dictionary<Guid, AnimalPosition> animalPositions = new Dictionary<Guid, AnimalPosition>();


    public void AddOrUpdatePosition(Guid animalId, AnimalPosition position)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            animalPositions[animalId] = position;
        }
        else
        {
            animalPositions.Add(animalId, position);
        }
    }


    public AnimalPosition GetPosition(Guid animalId)
    {
        if (animalPositions.TryGetValue(animalId, out AnimalPosition value))
        {
            return value;
        }
        else
        {
            return new AnimalPosition(-1, -1);
        }
    }


    public void RemovePosition(Guid animalId)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            animalPositions.Remove(animalId);
        }
    }
}