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
        //сбор данных из NUD'ов
        public static int[] collectNUD(params NumericUpDown[] NUDs)
        {
            int[] values = new int[NUDs.Length];
            for (int i = 0; i < NUDs.Length; i++)
            {
                values[i] = Convert.ToInt32(NUDs[i].Value);
            }
            //возвращение NUD'ов
            return values;
        }
    }
}
