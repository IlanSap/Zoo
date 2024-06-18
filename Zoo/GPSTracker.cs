using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class GPSTracker
{
    private  readonly Dictionary<Guid, Point> animalPositions = new Dictionary<Guid, Point>();


    public  void AddOrUpdatePosition(Guid animalId, Point position) {
        
        if (animalPositions.ContainsKey(animalId))
        {
            animalPositions[animalId] = position;
        }
        else
        {
            animalPositions.Add(animalId, position);
        }
    }

    public Point GetPosition(Guid animalId)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            return animalPositions[animalId];
        }
        else
        {
            return new Point(-1, -1);
        }
    }

    public  void RemovePosition(Guid animalId)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            animalPositions.Remove(animalId);
        }
    }
}