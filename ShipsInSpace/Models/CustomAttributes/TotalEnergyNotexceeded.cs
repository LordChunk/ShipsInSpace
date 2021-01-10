using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class TotalEnergyNotExceeded : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Ship needs to be created.");

            var ship = (ShipViewModel) value;

            if (ship.Energy < ship.GetTotalEnergyUsedByTheEquippedWeapons())
            {
                return new ValidationResult("The Equipped weapons drain more energy than the engine can support.");
            }

            return ValidationResult.Success;
        }
    }
}