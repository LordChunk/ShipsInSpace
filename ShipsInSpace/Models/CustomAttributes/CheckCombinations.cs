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

            return ValidationResult.Success;
        }

        private bool CheckImploderWeaponWithIntrepidEngine(ShipViewModel ship)
        {
            if (ship.Engine.Name == "Intrepid Class")
            {
                if (ship.Wings.SelectMany(wing => wing.Hardpoint).Any(weapon => weapon.Name == "Imploder"))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckForWeaponType(ShipViewModel ship, DamageTypeEnum damageType)
        {
            foreach (var wing in ship.Wings)
            {
                foreach (var weapon in wing.Hardpoint)
                {
                    if (weapon.DamageType == damageType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
