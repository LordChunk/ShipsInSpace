﻿using System.Collections.Generic;

namespace GalacticSpaceTransitAuthority
{
    public class Wing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Agility { get; set; }
        public int Speed { get; set; }
        public int Energy { get; set; }
        public int Weight { get; set; }
        public List<Weapon> Hardpoint { get; set; }
        public int NumberOfHardpoints { get; set; }

        public Wing Copy()
        {
            return (Wing)this.MemberwiseClone();
        }
    }
}
