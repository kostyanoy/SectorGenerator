using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class BuildingTypesSettings
    {
        public BuildingSettings[] BuildingSettings { get; set; } = new BuildingSettings[0]; //настройки лута в зданиях
        public string[] Types { get; set; } = new string[0]; //типы зданий
        public int[] TypesValues { get; set; } = new int[0]; //частота зданий
        public int[] ValuesChanges { get; set; } = new int[0]; //изменение чатсоты

    }
}
