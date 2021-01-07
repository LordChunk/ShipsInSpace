using System.Collections.Generic;
using System.Linq;
using GalacticSpaceTransitAuthority;

namespace ShipsInSpace.Models
{
    public class SelectionOfShipModel
    {
        public Ship SelectedShip { get; set; }

        public int NumberOfWings = 3;// { get; set; }

        public SelectionOfShipModel()
        {
            SelectedShip = new Ship();
        }
    }
}
