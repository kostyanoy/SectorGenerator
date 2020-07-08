using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    static class NUD
    {
        public static int[] collectNUD(params NumericUpDown[] NUDs)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                if (NUDs[i].Value > 0)
                    len++;
            }

            int[] values = new int[len];
            for (int i = 0; i < 9; i++)
            {
                if (NUDs[i].Value > 0)
                    values[i] = Convert.ToInt32(NUDs[i].Value);
            }

            return values;
        }
    }
}
