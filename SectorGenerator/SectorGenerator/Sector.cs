using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class Sector
    {
        public Sector(string name)
        {
            SectorName = name;
        }
        public string SectorName { get; set; }

        public string SectorInfo { get; set; } = "";

        public string[] ZonesNames { get; set; } = new string[1] { "" };

        public int[] ZonesShablon { get; set; } = new int[1] { 1 };

        public string[] ZonesInfo { get; set; } = new string[1] { "" };

        public string[][] BuildingsInfo { get; set; } = new string[1][] { new string[1] { "" } };
    }
}
