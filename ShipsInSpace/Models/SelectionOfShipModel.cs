using System.Collections.Generic;
using System.Linq;
using GalacticSpaceTransitAuthority;

namespace ShipsInSpace.Models
{
    public class SelectionOfShipModel
    {
        public Ship SelectedShip { get; set; }
        public List<Hull> AllHulls { get; set; }
        public List<Engine> AllEngines { get; set; }
        public List<Wing> AllWings { get; set; }
        public List<Weapon> AllWeapons { get; set; }

        public SelectionOfShipModel(ISpaceTransitAuthority spaceTransitAuthority)
        {
            AllHulls = spaceTransitAuthority.GetHulls().ToList();
            AllEngines = spaceTransitAuthority.GetEngines().ToList();
            AllWings = spaceTransitAuthority.GetWings().ToList();
            AllWeapons = spaceTransitAuthority.GetWeapons().ToList();
            SelectedShip = new Ship();
        }
    }
}
