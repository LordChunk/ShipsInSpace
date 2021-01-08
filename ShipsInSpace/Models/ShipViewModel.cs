using System.Collections.Generic;
using System.Linq;
using GalacticSpaceTransitAuthority;

namespace ShipsInSpace.Models
{
    public class ShipViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HullViewModel Hull { get; set; }
        public List<WingViewModel> Wings { get; set; }
        public EngineViewModel Engine { get; set; }

        public int GetTotalWeight() {
            var currentCapacity = 0;
    
            if (Engine != null)
            {
                currentCapacity += Engine.Weight;
            }

            if (Wings != null)
            {
                foreach (var wing in Wings)
                {
                    currentCapacity += wing.Weight;
                    currentCapacity += wing.Hardpoint.Sum(weapon => weapon.Weight);
                }
            }
            return currentCapacity;
        }
    }
}
