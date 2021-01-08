using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class TotalWeightNotExceeded : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return base.IsValid(value);
        }
    }
}
