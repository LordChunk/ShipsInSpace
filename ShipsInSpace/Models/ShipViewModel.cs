using System.Collections.Generic;
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
    }
}
