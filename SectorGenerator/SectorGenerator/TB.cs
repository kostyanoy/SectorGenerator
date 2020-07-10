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
        //сбор данных из TextBox'ов
        public static string[] collectTB(params TextBox[] TBoxes)
        { 
            string[] names = new string[TBoxes.Length];
            for (int i = 0; i < TBoxes.Length; i++)
            {
                names[i] = TBoxes[i].Text;
            }
            //возвращение массива Textbox'ов
            return names;
        }
    }
}
