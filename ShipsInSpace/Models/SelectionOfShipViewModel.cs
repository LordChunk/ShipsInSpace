using System.Collections.Generic;
using System.Linq;
using GalacticSpaceTransitAuthority;
using ShipsInSpace.Models.CustomAttributes;

namespace ShipsInSpace.Models
{
    public class SelectionOfShipViewModel
    {
        [TotalWeightNotExceeded]
        public ShipViewModel SelectedShip { get; set; }

        public int NumberOfWings = 3;// { get; set; }

        public SelectionOfShipViewModel()
        {
            SelectedShip = new ShipViewModel();
        }
    }
}
