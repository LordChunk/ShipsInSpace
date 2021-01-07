using System.Collections.Generic;
using GalacticSpaceTransitAuthority;

namespace ShipsInSpace.Models
{
    public class SpaceShip
    {
        public Hull Hull { get; set; }
        public Engine Engine { get; set; }
        public List<Wing> wings { get; set; }
    }
}
