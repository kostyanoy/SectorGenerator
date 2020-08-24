using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class SectorDangerLevel
    {
        public SectorSpecSettings[] SpecSettings { get; set; } = new SectorSpecSettings[0];

        public SectorNamesSettings NamesSettings { get; set; } = new SectorNamesSettings();
    }
}
