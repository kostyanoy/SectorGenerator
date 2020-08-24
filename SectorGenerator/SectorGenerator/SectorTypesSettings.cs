using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    class SectorTypesSettings : Settings
    {
        public string[] Types { get; set; } = new string[0]; //массив типов секторов
        public int[] Values { get; set; } = new int[0]; //частота типов секторов
        public int[] MinZones { get; set; } = new int[0]; //минимальное количество зон
        public int[] MaxZones { get; set; } = new int[0]; //максимальное количество зон


        public int ChoosedIndex { get; set; } = 0; //выбранный индекс
        public int Zones { get; set; } = 0; //сгененрированное кол-во зон

        //выбор из массива типов
        public string Choose()
        {
            string result = TB.getName(Types, Values); //выбор типа
            int ind = FindIndex(Types, result); //индекс типа сектора
            int minZone = MinZones[ind];
            int maxZone = MaxZones[ind];

            if(minZone > maxZone) //замена мин и макс
            {
                int f = minZone;
                minZone = maxZone;
                maxZone = f;
            }

            ChoosedIndex = ind;
            Zones = ran.Next(minZone, maxZone); //генерация количества зон
            return result;
        }

        //сбор типов, введенных игроком
        public void Collect(string[] types, int[] values, int[] minZones, int[] maxZones)
        {
            int count = 0; //подсчёт кол-ва непустых
            int ind = 0; //индекс в массиве типов
            foreach (string type in types)
            //подчёт введенных типов
            {
                if (type != "")
                    count++;
            }
            //создание массивов
            Types = new string[count];
            Values = new int[count];
            MinZones = new int[count];
            MaxZones = new int[count];
            //запись в массивы
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] != "")
                {
                    Types[ind] = types[i];
                    Values[ind] = values[i];
                    MinZones[ind] = minZones[i];
                    MaxZones[ind] = maxZones[i];

                    ind++;
                }
            }
        }

        //запись на обозрение игрока
        public void Input(TextBox[] TB, NumericUpDown[] NUDv, NumericUpDown[] NUDmin, NumericUpDown[] NUDmax)
        {
            for (int i = 0; i < Types.Length; i++)
            {
                TB[i].Text = Types[i];
                NUDv[i].Value = Values[i];
                NUDmin[i].Value = MinZones[i];
                NUDmax[i].Value = MaxZones[i];
            }
        }
    }
}
