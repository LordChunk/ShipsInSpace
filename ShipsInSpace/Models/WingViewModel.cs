using System.Collections.Generic;
using ShipsInSpace.Models;

namespace ShipsInSpace.Models
{
    public class WingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Agility { get; set; }
        public int Speed { get; set; }
        public int Energy { get; set; }
        public int Weight { get; set; }
        public List<WeaponViewModel> Hardpoint { get; set; }
        public int NumberOfHardpoints { get; set; }
    }
}
