using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorGenerator
{
    class Settings
    {
        public Random ran = new Random(); //рандом

        //находление индекса элемента в массиве
        public int FindIndex(string[] names, string name)
        {
                        for (int i = 0; i<names.Length; i++)
            {
                if (names[i] == name)
                    return i;
            }
            //если строки не было в массиве
            return 0;
        }

        //генератор врагов и лута
        public string itemsGenerator(string[] items, int[] before, int[] after, int chance, bool cycled, bool interrupt)
        {
            //сбор нужных параметров
            string choosedItems = "";

            int i = -1;
            while (!(!cycled && i == items.Length - 1))
            {
                i++;
                if (i == items.Length)
                    i = 0;
                if (items[i] == "")
                    continue;
                chance -= before[i];
                if (chance > ran.Next(100))
                {
                    choosedItems += items[i] + "\n";
                    chance -= after[i];
                }
                else
                {
                    chance += before[i];
                    if (interrupt || before[i] == 0)
                        cycled = false;
                }
            }
            return choosedItems;
        }

        //получение случайного порядка индексов
        public int[] getOrder(string[] names, int len)
        {
            //массив из len чисел с 0
            int[] mas = new int[len];
            for (int i = 0; i < len; i++) { mas[i] = i; }
            int[] newMas = new int[len]; //массив, в который заносится случайный порядок
            int st = 0; //индекс для непустых TwxtBox'ов
            int en = mas.Length - 1; //индекс для пустых TextBox'ов
            int ind; //случайный индекс
            for (int i = 0; i < len; i++)
            {
                ind = ran.Next(mas.Length); //генерация случайного числа
                if (names[mas[ind]] == "") //если путой Textbox
                {
                    newMas[en] = mas[ind];
                    en--;
                }
                else //если непустой Textbox
                {
                    newMas[st] = mas[ind];
                    st++;
                }
                mas = delete(mas, ind); //удаление использованного числа        
            }
            return newMas; //возвращение массива со случайным порядком индексов
        }
        //удаление элемента из массива
        public int[] delete(int[] mas, int ind)
        {
            int[] newMas = new int[mas.Length - 1];
            int j = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                if (i == ind)
                    continue;
                newMas[j] = mas[i];
                j++;
            }
            return newMas;
        }
        //перемешивание string массива
        public string[] reshuffle(string[] mas, int[] order)
        {
            string[] newMas = new string[mas.Length];
            for (int i = 0; i < mas.Length; i++)
            {
                newMas[i] = mas[order[i]];
            }
            return newMas;
        }
        //перемешивание int массива
        public int[] reshuffle(int[] mas, int[] order)
        {
            int[] newMas = new int[mas.Length];
            for (int i = 0; i < mas.Length; i++)
            {
                newMas[i] = mas[order[i]];
            }
            return newMas;
        }



    }
}
