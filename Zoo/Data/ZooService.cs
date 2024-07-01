using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooProject.Data;

public class ZooService
{
    private readonly ZooContext _context;

    public ZooService(ZooContext context)
    {
        _context = context;
    }


    public void AddZoo(Zoo.Zoo zoo)
    {
        if (_context.Entry(zoo).State == EntityState.Detached)
        {
            _context.Zoos.Attach(zoo);
        }
        else
        {
            _context.Zoos.Add(zoo);
        }

        foreach (var animal in zoo._animals)
        {
            if (_context.Entry(animal).State == EntityState.Detached)
            {
                _context.Animals.Attach(animal);
            }
            else
            {
                _context.Animals.Add(animal);
            }
        }

        _context.SaveChanges();
    }

    public void UpdateZoo(Zoo.Zoo zoo)
    {
        _context.Zoos.Update(zoo);
        _context.SaveChanges();
    }

    public List<Zoo.Zoo> GetAllZoos()
    {
        return _context.Zoos.Include(z => z._animals).ToList();
    }
}

