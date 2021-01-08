using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class TotalWeightNotExceeded : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Ship needs to be created.");

            var ship = (ShipViewModel)value;
            
            if (ship.Hull == null)
                return new ValidationResult("Hull needs to be selected.");
           
            var maxCapacity = (int)ship.Hull.DefaultMaximumTakeOffMass;
            int currentCapacity = 0;

            if (ship.Engine != null)
            {
                currentCapacity += ship.Engine.Weight;
            }

            if (ship.Wings != null)
            {
                currentCapacity += ship.Wings.Sum(wing => wing.Weight);
            }

            return currentCapacity > maxCapacity ? ValidationResult.Success : new ValidationResult("Current configuration weights too much!");
        }
    }
}
