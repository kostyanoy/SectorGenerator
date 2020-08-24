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

        //выбор значения из TextBox'ов на основе частоты
        public static string getName(string[] names, int[] values)
        {
            Random ran = new Random();
            int sum = 0;
            int maxNumber = 0;
            //складывание всех частот
            for (int i = 0; i < names.Length; i++)
            {
                if (values[i] == 0 || names[i] == "")
                    continue;
                maxNumber += values[i];
            }
            //"бросок кубика"
            int number = ran.Next(maxNumber);
            for (int i = 0; i < names.Length; i++)
            {
                if (values[i] == 0 || names[i] == "")
                    continue;
                sum += values[i];
                if (sum > number)
                    return names[i];
            }
            return "";
        }
    }


}
