using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectorGenerator
{
    public partial class Form1 : Form
    {
        Random ran = new Random();
        int counter = 0;
        public Form1()
        {
            InitializeComponent();
        }

        //генерация сектора
        private void button1_Click(object sender, EventArgs e)
        {
            //сбор данных во вкладке "Генерация сектора"
            string[] sectorNames = TB.collectTB(TBsectorName1, TBsectorName2, 
                TBsectorName3, TBsectorName4, TBsectorName5, TBsectorName6, 
                TBsectorName7, TBsectorName8, TBsectorName9);
            int[] sectorNameValues = NUD.collectNUD(NUDsectorName1, NUDsectorName2,
                NUDsectorName3, NUDsectorName4, NUDsectorName5, NUDsectorName6,
                NUDsectorName7, NUDsectorName8, NUDsectorName9);
            string[] sectorTypes = TB.collectTB(TBsectorType1, TBsectorType2,
                TBsectorType3, TBsectorType4, TBsectorType5, TBsectorType6,
                TBsectorType7, TBsectorType8, TBsectorType9);
            int[] sectorMinZones = NUD.collectNUD(NUDsectorMinZones1, NUDsectorMinZones2,
                NUDsectorMinZones3, NUDsectorMinZones4, NUDsectorMinZones5, NUDsectorMinZones6,
                NUDsectorMinZones7, NUDsectorMinZones8, NUDsectorMinZones9);
            int[] sectorMaxZones = NUD.collectNUD(NUDsectorMaxZones1, NUDsectorMaxZones2,
                NUDsectorMaxZones3, NUDsectorMaxZones4, NUDsectorMaxZones5, NUDsectorMaxZones6,
                NUDsectorMaxZones7, NUDsectorMaxZones8, NUDsectorMaxZones9);
            int[] sectorTypeValues = NUD.collectNUD(NUDsectorType1, NUDsectorType2,
                NUDsectorType3, NUDsectorType4, NUDsectorType5, NUDsectorType6,
                NUDsectorType7, NUDsectorType8, NUDsectorType9);

            //генерация сектора
            string sectorInfo; //информация о секторе во вкладку Информация О Секторе
            string result; //результат генерации
            //выбор спецификации
            string sectorName = getName(sectorNames, sectorNameValues);
            if (sectorName == "") 
            {
                LBLsectorHint.Text = "Введите возможные спецификации сектора и укажите их частоту";
                return; 
            }
            sectorInfo = "Спецификация:\n" + sectorName + "\n\n";
            //выбор типа
            string sectorType = getName(sectorTypes, sectorTypeValues);
            if (sectorType == "") 
            {
                LBLsectorHint.Text = "Введите возможные типы сектора и укажите их частоту";
                return; 
            }
            sectorInfo += "Тип:\n" + sectorType + "\n\n";
            int sectorTypeNum = findIndex(sectorTypes, sectorType); //индекс сектора
            //количество зон
            int minZones = sectorMinZones[sectorTypeNum];
            int maxZones = sectorMaxZones[sectorTypeNum];
            string Zones;
            if (minZones <= maxZones)
                Zones = ran.Next(minZones, maxZones+1).ToString();
            else
                Zones = ran.Next(maxZones, minZones + 1).ToString();
            sectorInfo += "Количество зон:\n" + Zones + "\n\n";

            //запись данных
            result = sectorName + " " + sectorType + " " + Zones + " Зон";
            LBLsectorResult.Text = result;
            LBLsectorHint.Text = "Теперь перейдите во вкладку Генератор Зон \n\n Информацию о секторе можно посмотреть\n во вкладке Информация О Секторе ";
            NUDzoneCount.Value = Convert.ToInt32(Zones);
            LBLinfoSectorInfo.Text = sectorInfo;
        }       
        
        //поиск индекса в массиве
        private int findIndex(string[] names, string name)
        {
            for(int i = 0; i < names.Length; i++)
            {
                if (names[i] == name)
                    return i;
            }
            //если строки не было в массиве
            return 0; 
        }

        //выбор значения из TextBox'ов на основе частоты
        private string getName(string[] names, int[] values)
        {
            int sum = 0;
            int maxNumber = 0;
            //складывание всех частот
            for(int i = 0; i < names.Length; i++) 
            {
                if (values[i] == 0 || names[i] == "")
                    continue;
                maxNumber += values[i];
            }
            //"бросок кубика"
            int number = ran.Next(maxNumber);
            for(int i = 0; i < names.Length; i++)
            {
                if(values[i] == 0 || names[i] == "") 
                    continue;
                sum += values[i];
                if (sum > number)  
                    return names[i];  
            }
            return "";
        }



        //генерация зоны
        private void BTNzoneResult_Click(object sender, EventArgs e)
        {
            //сбор данных во вкладке "Генерация Зоны"
            int[] zoneEnemyBeforeSpawn = NUD.collectNUD(NUDbeforeSpawn1, NUDbeforeSpawn2, 
                NUDbeforeSpawn3, NUDbeforeSpawn4, NUDbeforeSpawn5, NUDbeforeSpawn6, 
                NUDbeforeSpawn7, NUDbeforeSpawn8, NUDbeforeSpawn9, NUDbeforeSpawn10);
            int[] zoneEnemyAfterSpawn = NUD.collectNUD(NUDafterSpawn1, NUDafterSpawn2,
                NUDafterSpawn3, NUDafterSpawn4, NUDafterSpawn5, NUDafterSpawn6,
                NUDafterSpawn7, NUDafterSpawn8, NUDafterSpawn9, NUDafterSpawn10);
            string[] zoneEnemyNames = TB.collectTB(TBzoneEnemy1, TBzoneEnemy2, 
                TBzoneEnemy3, TBzoneEnemy4, TBzoneEnemy5, TBzoneEnemy6, 
                TBzoneEnemy7, TBzoneEnemy8, TBzoneEnemy9, TBzoneEnemy10);


            
            string enemy = enemiesGenerator(zoneEnemyNames, zoneEnemyBeforeSpawn, zoneEnemyAfterSpawn);

            string shablon = "Шаблон №" + (ran.Next(Convert.ToInt32(NUDzoneTypes.Value)) + 1).ToString();
            string side = "Перед смотрит на " + chooseSide();
            string enemies = "Враги в зоне: \n";
            if (enemy.Length == 0)
                enemies += "Отсутствуют";
            
            

            //запись данных
            LBLzoneInfo.Text = "Теперь перейдите во вкладку Генератор Зданий \n\n Информацию о зонах можно посмотреть\n во вкладке Информация О Секторе";
            LBLzoneInfoGenerated.Text = $"Сгенерировано {NUDzoneCount.Value} Зон";
            label1.Text = enemy;

        }

        //генератор врагов
        private string enemiesGenerator(string[] enemies, int[] before, int[] after)
        {
            //сбор нужных параметров
            string zoneEnemies = "";
            int chance = Convert.ToInt32(NUDstartChance.Value);
            bool cycled = CBzoneCycled.Checked;
            bool interupt = CBzoneInterrupt.Checked;

            int i = -1;
            while (!(!cycled && i == enemies.Length - 1))
            {
                i++;
                if (i == enemies.Length)
                    i = 0;
                if (enemies[i] == "")
                    continue;
                chance -= before[i];
                if (chance > ran.Next(100))
                {
                    zoneEnemies += enemies[i] + "\n";
                    chance -= after[i];
                }
                else
                {
                    chance += before[i];
                    if (interupt || before[i] == 0)
                        cycled = false;

                }
                
           
            }

            return zoneEnemies;
        }

        //выбор стороны, в которую смотрит зона
        private string chooseSide()
        {
            if (CLBzonesSides.CheckedItems.Count > 0)
            {
                int i = ran.Next(CLBzonesSides.CheckedItems.Count);
                return CLBzonesSides.CheckedItems[i].ToString();
            }
            //если не поставлена галочка нигде
            return "Стороны не выбраны";
        }

        //нажатие на кнопку "Если скучно"
        private void BTNzoneCounter_Click(object sender, EventArgs e)
        {
            counter++;
            LBLzoneCounter.Text = counter.ToString();//добавление единицы
        }
    }
}
