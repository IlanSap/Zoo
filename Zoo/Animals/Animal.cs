using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ZooProject.Animals.AnimalTypes;


public class Animal : IAnimal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AnimalId { get; set; }

    public int StepSize { get; set; }

    public System.ConsoleColor AnimalBackgroundColor { get; set; }
    public System.ConsoleColor AnimalForegroundColor { get; set; }


    [Required]
    public AnimalType AnimalType { get; set; }

    public void Move(Func<Animal, bool> moveFn)
    {
        bool success = moveFn.Invoke(this);
        /*        if (!success)
                    Console.WriteLine($"Animal {this.AnimalType.ToString()} couldn't find a place to move.");*/
    }

    public Animal(AnimalType animalType, int stepSize=1, ConsoleColor animalBackgroundColor= ConsoleColor.Black, ConsoleColor animalForegroundColor= ConsoleColor.White)
    {
        AnimalId = Guid.NewGuid(); // Ensure AnimalId is unique
        AnimalType = animalType;
        StepSize = stepSize;
        AnimalBackgroundColor = animalBackgroundColor;
        AnimalForegroundColor = animalForegroundColor;
    }
}

