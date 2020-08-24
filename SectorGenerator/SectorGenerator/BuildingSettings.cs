using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class BuildingSettings
    {
        public string Name { get; set; } = "";

        public string[] LootNames { get; set; } = new string[0];

        public int[] LootBeforeSpawn { get; set; } = new int[0];

        public int[] LootAfterSpawn { get; set; } = new int[0];

        public int StaringChance { get; set; } = 0;

        public bool Reshuffle { get; set; } = false;

        public bool Cycled { get; set; } = false;

        public bool Interrupt { get; set; } = false;

    }
}
