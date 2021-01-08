using System.ComponentModel.DataAnnotations;

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
           
            return ship.GetTotalWeight() > (int)ship.Hull.ActualTakeOffMass ? ValidationResult.Success : new ValidationResult("Current configuration weights too much!");
        }
    }
}
