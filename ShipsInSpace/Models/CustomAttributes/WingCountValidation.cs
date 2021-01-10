using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class WingCountValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var wingCount = (int) value;

            if (wingCount < 2) return new ValidationResult("You need more wings, boyo");

            if (wingCount % 2 != 0)
            {
                return new ValidationResult(
                    "There are an odd number off wings. Have you ever seen a spaceship with an odd number off wings? me neither so change it to an even number of wings, thanks in advance.");
            }

            return ValidationResult.Success;
        }
    }
}