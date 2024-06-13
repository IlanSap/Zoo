using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GPSTracker
{
    public static Dictionary<Guid, AnimalPosition> animalPositions = new Dictionary<Guid, AnimalPosition>();

    public static Dictionary<Guid, Zoo> animalZoos = new Dictionary<Guid, Zoo>();

    
    /// //////////////////////////////////////////////////////////////////
    public static void AddOrUpdateZoo(Guid animalId, Zoo zoo)
    {
        if (animalZoos.ContainsKey(animalId))
        {
            animalZoos[animalId] = zoo;
        }
        else
        {
            animalZoos.Add(animalId, zoo);
        }
    }

    public static Zoo GetZoo(Guid animalId)
    {
        if (animalZoos.ContainsKey(animalId))
        {
            return animalZoos[animalId];
        }
        else
        {
            return null;
        }
    }

    public static void RemoveZoo(Guid animalId)
    {
        if (animalZoos.ContainsKey(animalId))
        {
            animalZoos.Remove(animalId);
        }
    }

    /// //////////////////////////////////////////////////////////////////

    public static void AddOrUpdatePosition(Guid animalId, AnimalPosition position)
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

    public static AnimalPosition GetPosition(Guid animalId)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            return animalPositions[animalId];
        }
        else
        {
            return new AnimalPosition(-1, -1);
        }
    }

    public static void RemovePosition(Guid animalId)
    {
        if (animalPositions.ContainsKey(animalId))
        {
            animalPositions.Remove(animalId);
        }
    }
}