using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    class SectorNamesSettings : Settings
    {
        public string[] Names { get; set; } = new string[0]; //специализации      
        public int[] Values { get; set; } = new int[0]; //частота


        public int ChoosedIndex { get; set; } = 0; //выбранный индекс

        //выбор из списка специализаций
        public string Choose() 
        {
            string result = TB.getName(Names, Values);
            int ind = FindIndex(Names, result); //индекс типа сектора
            ChoosedIndex = ind;
            return result;
        }

        //сбор специализаций, введённых игроком
        public void Collect(string[] names, int[] values)
        {
            int count = 0; //количество не пустых
            int ind = 0; //индекс массивов
            //подсчёт количества
            foreach (string name in names)
            {
                if (name != "")
                    count++;
            }
            //создание массивов
            Names = new string[count];
            Values = new int[count];
            //запись данных
            for (int i = 0; i < names.Length; i++)
            {
                if(names[i] != "")
                {
                    Names[ind] = names[i];
                    Values[ind] = values[i];

                    ind++;
                }
            }
        }

        //запись на обозрение игрока
        public void Input(TextBox[] TB, NumericUpDown[] NUD)
        {
            for(int i = 0; i < Names.Length; i++)
            {
                TB[i].Text = Names[i];
                NUD[i].Value = Values[i];
            }
        }
    }
}
