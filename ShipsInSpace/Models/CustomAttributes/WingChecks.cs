using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class WingChecks : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var wingsList = (List<WingViewModel>)value;

            if (wingsList.Count % 2 != 0)
            {
                return new ValidationResult("There are an odd number off wings. Have you ever seen a spaceship with an odd number off wings? me neither so change it to an even number of wings, thanks in advance.");
            }

            if (wingsList.Any(wing => wing.Hardpoint.Count > wing.NumberOfHardpoints))
            {
                return new ValidationResult("You cant equip more weapons than there are slots on the wing, bloody wanker!");
            }

            if (CheckKineticWeapons(wingsList))
            {
                return new ValidationResult("The difference between the energy usage of kinetic weapons on each wing are higher than 35!");
            }

            return ValidationResult.Success;
        }

        private bool CheckKineticWeapons(List<WingViewModel> wingsList)
        {
            List<int> energyUsedByKineticWeaponsPerWing = new List<int>();

            foreach (var wing in wingsList)
            {
                int energyUsedByKineticWeaponsOnWing = 0;

                foreach (var weapon in wing.Hardpoint)
                {
                    if (weapon.DamageType == DamageTypeEnum.Kinetic)
                    {
                        energyUsedByKineticWeaponsOnWing += weapon.EnergyDrain;
                    }
                }
                energyUsedByKineticWeaponsPerWing.Add(energyUsedByKineticWeaponsOnWing);
            }

            energyUsedByKineticWeaponsPerWing.Sort();

            return Math.Abs(energyUsedByKineticWeaponsPerWing[0] - energyUsedByKineticWeaponsPerWing[energyUsedByKineticWeaponsPerWing.Count - 1]) > 35;
        }
    }
}
