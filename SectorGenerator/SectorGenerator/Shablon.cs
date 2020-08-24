using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class Shablon
    {
        public Shablon(string name, int buildings)
        {
            Name = name;
            Buildings = buildings;
        }
        public string Name { get; set; } = "";
        public int Buildings { get; set; } = 5;
    }
}
