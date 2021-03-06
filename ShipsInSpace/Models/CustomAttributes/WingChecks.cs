﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShipsInSpace.Models.CustomAttributes
{
    public class WingChecks : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null) return ValidationResult.Success;

            var wingsList = (List<WingViewModel>)value;

            if (wingsList.Any(w => w.Name == null))
            {
                return ValidationResult.Success;
            }

            if (wingsList.Count == 0) return ValidationResult.Success;

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
