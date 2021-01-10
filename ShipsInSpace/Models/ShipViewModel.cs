using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ShipsInSpace.Models.CustomAttributes;

namespace ShipsInSpace.Models
{
    [TotalWeightNotExceeded]
    [CheckCombinations]
    [TotalEnergyNotExceeded]
    public class ShipViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HullViewModel Hull { get; set; }


        [Display(Name = "Number of wings")]
        [WingCountValidation]
        public int NumberOfWings { get; set; }

        [WingChecks]
        public List<WingViewModel> Wings { get; set; }
        public EngineViewModel Engine { get; set; }

        public int GetTotalWeight() {
            var currentCapacity = 0;
            double multiplier = 1;

            if (Engine != null)
            {
                currentCapacity += Engine.Weight;
            }

            if (Wings != null && Wings.All(w => w.Name != null))
            {
                if (TwoOrMoreStatisRiflesEquiped())
                {
                    multiplier = 0.85;
                }

                foreach (var wing in Wings)
                {
                    currentCapacity += wing.Weight;
                    currentCapacity += wing.Hardpoint.Sum(weapon => weapon.Weight);
                }
            }
            return currentCapacity * (int)multiplier;
        }

        private bool TwoOrMoreStatisRiflesEquiped()
        {
            int amountOfStasisRifles = 0;
            foreach (var wing in Wings)
            {
                foreach (var weapon in wing.Hardpoint)
                {
                    if (weapon.DamageType == DamageTypeEnum.Statis)
                    {
                        amountOfStasisRifles++;
                    }
                }
            }

            return amountOfStasisRifles >= 2;
        }

        public int GetTotalEnergyUsedByTheEquippedWeapons()
        {
            var currentEnergy = 0;
            
            if (Wings != null && Wings.All(w => w.Name != null))
            {
                Dictionary<DamageTypeEnum, int> amountOfWeaponsPerDamageType = AmountOfWeaponsPerDamageType();

                foreach (var wing in Wings)
                {
                    foreach (var weapon in wing.Hardpoint)
                    {
                        if (amountOfWeaponsPerDamageType[weapon.DamageType] >= 3)
                        {
                            currentEnergy += (int)(weapon.EnergyDrain * 0.8);
                        }
                        else
                        {
                            currentEnergy += weapon.EnergyDrain;
                        }
                    }
                }
            }
            return currentEnergy;
        }

        private Dictionary<DamageTypeEnum, int> AmountOfWeaponsPerDamageType()
        {
            Dictionary<DamageTypeEnum, int> amountOfWeaponsPerDamageType = new Dictionary<DamageTypeEnum, int>();

            foreach (var damageType in Enum.GetValues<DamageTypeEnum>())
            {
                amountOfWeaponsPerDamageType.Add(damageType,0);
            }

            foreach (var wing in Wings)
            {
                foreach (var weapon in wing.Hardpoint)
                {
                    amountOfWeaponsPerDamageType[weapon.DamageType]++;
                }
            }

            return amountOfWeaponsPerDamageType;
        }
    }
}
