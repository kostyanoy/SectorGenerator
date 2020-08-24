using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    class SectorSpecSettings : Settings
    {
        
        BuildingTypesSettings BuildingTypes { get; set; } = new BuildingTypesSettings(); //настройки типов зданий

        public SectorTypesSettings TypesSettings { get; set; } = new SectorTypesSettings(); //настройки типов секторов

        //враги
        public string Name { get; set; } = ""; //специализация
        public string[] EnemyNames { get; set; } = new string[0]; //названия врагов
        public int[] EnemyBeforeSpawn { get; set; } = new int[0]; //уменьшение до появления
        public int[] EnemyAfterSpawn { get; set; } = new int[0]; //уменьшение после пояления
        public int StartingChance { get; set; } = 0; //начальный шанс
        public bool Reshuffle { get; set; } = false; //перемешивание
        public bool Cycled { get; set; } = false; //цикличность
        public bool Interrupt { get; set; } = false; //прерывание
        public bool[] Sides { get; set; } = new bool[0]; //отмеченные стороны
        public Shablon[] ShablonsInfo { get; set; } = new Shablon[0]; //шаблоны

        public void Collect(string name, string[] enemyNames, int[] enemyBefore, int[] enemyAfter, int chance, bool reshufle, bool cycled, bool interrupt, bool[] sides, Shablon[] shablons)
        {
            int count = 0;
            Name = name;
            for(int i = 0; i < enemyNames.Length; i++)
            {
                if (enemyNames[i] != "")
                    count++;
            }
            EnemyNames = new string[count];
            EnemyAfterSpawn = new int[count];
            EnemyBeforeSpawn = new int[count];

            int ind = 0;
            for (int i = 0; i < enemyNames.Length; i++)
            {
                if(enemyNames[i] != "")
                {
                    EnemyNames[ind] = enemyNames[i];
                    EnemyAfterSpawn[ind] = enemyAfter[i];
                    EnemyBeforeSpawn[ind] = enemyBefore[i];
                }
                
            }

            StartingChance = chance;
            Reshuffle = reshufle;
            Cycled = cycled;
            Interrupt = interrupt;
            Sides = sides;
            ShablonsInfo = shablons;
        }

        public void Input(TextBox[] enemyNames, NumericUpDown[] enemyBefore, NumericUpDown[] enemyAfter, NumericUpDown chance, CheckBox reshufle, CheckBox cycled, CheckBox interrupt, CheckedListBox sides)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < EnemyNames.Length)
                {
                    enemyNames[i].Text = EnemyNames[i];
                    enemyAfter[i].Value = EnemyAfterSpawn[i];
                    enemyBefore[i].Value = EnemyBeforeSpawn[i];
                }
                else
                {
                    enemyNames[i].Text = "";
                    enemyAfter[i].Value = 0;
                    enemyBefore[i].Value = 0;
                }
            }
            chance.Value = StartingChance;
            reshufle.Checked = Reshuffle;
            interrupt.Checked = Interrupt;
            cycled.Checked = Cycled;

            for(int i = 0; i < sides.Items.Count; i++)
            {
                sides.SetItemChecked(i, Sides[i]);
            }
            
        }

        public Zone[] Generate(int zonesNum)
        {
            string[] enemyNames = EnemyNames;
            int[] enemyBeforeSpawn = EnemyBeforeSpawn;
            int[] enemyAfterSpawn = EnemyAfterSpawn;
            string enemy, enemies, shablonInfo, side;
            Zone[] zones = new Zone[zonesNum];
            for (int i = 0; i < zonesNum; i++)
            {
                enemy = itemsGenerator(EnemyNames, EnemyBeforeSpawn, EnemyAfterSpawn,
                    StartingChance, Cycled, Interrupt);
                int shablonNum = ran.Next(ShablonsInfo.Length) + 1; //номер шаблона
                zones[i].Shablon = shablonNum;
                shablonInfo = "Шаблон №" + shablonNum.ToString() + "\nЗданий: " + ShablonsInfo[shablonNum - 1].Buildings + "\n\n";
                side = "Перед смотрит на " + chooseSide() + "\n\n";
                enemies = "Враги в зоне: \n";
                if (enemy.Length == 0)
                    enemies += "Отсутствуют";
                else
                    enemies += enemy;

                if (Reshuffle)
                {
                    int[] order = getOrder(EnemyNames, EnemyNames.Length);
                    enemyNames = reshuffle(enemyNames, order);
                    enemyBeforeSpawn = reshuffle(enemyBeforeSpawn, order);
                    enemyAfterSpawn = reshuffle(enemyAfterSpawn, order);
                }
                //запись данных
                zones[i].Info = shablonInfo + side + enemies;
                string zoneName = "Зона " + (i + 1).ToString();
                zones[i].Name = zoneName;
                zones[i].Buildings = new Building[ShablonsInfo[shablonNum - 1].Buildings];
            }
            return zones;
        }

        //выбор стороны, в которую смотрит зона
        private string chooseSide()
        {
            int count = 0;
            int ind = 0;
            foreach(bool side in Sides)
            {
                if (side)
                    count++;                    
            }
            string[] selected = new string[count];
            for(int j = 0; j < Sides.Length; j++)
            {
                if (Sides[j])
                {
                    switch (j)
                    {
                        case 0:
                            selected[ind] = "Восток";
                            break;
                        case 1:
                            selected[ind] = "Запад";
                            break;
                        case 2:
                            selected[ind] = "Север";
                            break;
                        case 3:
                            selected[ind] = "Юг";
                            break;
                    }
                    ind++;
                }
                    

            }
            int k = ran.Next(selected.Length);
            return selected[k];

        }

        //перемешивание 
        public void ReshuffleNames()
        {
            string[] enemyNames = EnemyNames;
            int[] enemyBeforeSpawn = EnemyBeforeSpawn;
            int[] enemyAfterSpawn = EnemyAfterSpawn;
            //получение случайного порядка индексов
            int[] order = getOrder(EnemyNames, EnemyNames.Length);
            //присваиваем Textbox'ам и NUD'ам значения
            for (int i = 0; i < order.Length; i++)
            {
                EnemyNames[i] = enemyNames[order[i]];
                if (EnemyNames[i] != "")
                {
                    EnemyBeforeSpawn[i] = enemyBeforeSpawn[order[i]];
                    EnemyAfterSpawn[i] = enemyAfterSpawn[order[i]];
                }
                else //0,0 если пустой TextBox
                {
                    EnemyBeforeSpawn[i] = 0;
                    EnemyAfterSpawn[i] = 0;
                }
            }
        }
    }
}
