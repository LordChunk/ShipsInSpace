using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class CheckCombinations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ship = (ShipViewModel)value;

            if (CheckImploderWeaponWithIntrepidEngine(ship))
            {
                return new ValidationResult("You cant use the weapon imploder in combination with the Intrepid Class engine.");
            }
            
            if (CheckForWeaponType(ship, DamageTypeEnum.Cold) && CheckForWeaponType(ship, DamageTypeEnum.Heat))
            {
                return new ValidationResult("You cant use cold weapon(s) in combination with heat weapon(s).");
            }

            if (CheckForWeaponType(ship, DamageTypeEnum.Gravity) && CheckForWeaponType(ship, DamageTypeEnum.Statis))
            {
                return new ValidationResult("You cant use stasis weapon(s) in combination with gravity weapon(s).");
            }

            if (NullifierCantBeAlloneOnWing(ship))
            {
                return new ValidationResult("You cant put the Nullifier weapon as the only weapon on a wing. Add another weapon if possible or change weapon/wing.");
            }

            return ValidationResult.Success;
        }

        private bool CheckImploderWeaponWithIntrepidEngine(ShipViewModel ship)
        {
            return ship.Engine.Name == "Intrepid Class" && ship.Wings.SelectMany(wing => wing.Hardpoint).Any(weapon => weapon.Name == "Imploder");
        }

        private bool CheckForWeaponType(ShipViewModel ship, DamageTypeEnum damageType)
        {
            return ship.Wings.Any(wing => wing.Hardpoint.Any(weapon => weapon.DamageType == damageType));
        }

        private bool NullifierCantBeAlloneOnWing(ShipViewModel ship)
        {
            return ship.Wings.Any(wing => wing.Hardpoint.Capacity == 1 && wing.Hardpoint[0].Name == "Nullifier");
        }
    }
}
