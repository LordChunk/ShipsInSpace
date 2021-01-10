using System.Collections.Generic;
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

        [WingChecks]
        public List<WingViewModel> Wings { get; set; }
        public EngineViewModel Engine { get; set; }

        public int GetTotalWeight() {
            var currentCapacity = 0;
    
            if (Engine != null)
            {
                currentCapacity += Engine.Weight;
            }

            if (Wings != null && Wings.All(w => w.Name != null))
            {
                foreach (var wing in Wings)
                {
                    currentCapacity += wing.Weight;
                    currentCapacity += wing.Hardpoint.Sum(weapon => weapon.Weight);
                }
            }
            return currentCapacity;
        }

        public int GetTotalEnergyUsedByTheEquippedWeapons()
        {
            var currentEnergy = 0;
            
            if (Wings != null && Wings.All(w => w.Name != null))
            {
                foreach (var wing in Wings)
                {
                    currentEnergy += wing.Hardpoint.Sum(weapon => weapon.EnergyDrain);
                }
            }
            return currentEnergy;
        }
    }
}
