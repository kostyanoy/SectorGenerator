﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class Sector : Obj
    {
        public Sector(string name)
        {
            Name = name;
        }

        public Zone[] Zones = new Zone[0] {};

        public string Spec { get; set; } = "";

        public int Level { get; set; } = 1;
    }
}
