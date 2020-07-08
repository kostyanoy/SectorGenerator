using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    static class TB
    {
        public static string[] collectTB(params TextBox[] TBoxes)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                if (TBoxes[i].Text != "")
                    len++;
            }
            string[] names = new string[len];
            for (int i = 0; i < 9; i++)
            {
                if (TBoxes[i].Text != "")
                    names[i] = TBoxes[i].Text;
            }

            return names;
        }
    }
}
