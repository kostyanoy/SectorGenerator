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
        int counter = 0; //счётчик нажатий на кнопку
        int[] shablon = {5, 5, 5}; //количество зданий в шаблонах
        int[] zonesShablon; //номер шаблона каждой зоны
        string[] zonesInfo; //информация о каждой зоне
        string[][] buildingsInfo; //список списков зданий в каждой зоне

        public Form1()
        {
            InitializeComponent();
        }

        //генерация сектора
        private void button1_Click(object sender, EventArgs e)
        {
            //сбор данных во вкладке "Генерация сектора"
            TextBox[] TBsectorNames = {TBsectorName1, TBsectorName2,
                TBsectorName3, TBsectorName4, TBsectorName5, TBsectorName6,
                TBsectorName7, TBsectorName8, TBsectorName9};
            string[] sectorNames = TB.collectTB(TBsectorNames);
            NumericUpDown[] NUDsectorNameValues = {NUDsectorName1, NUDsectorName2,
                NUDsectorName3, NUDsectorName4, NUDsectorName5, NUDsectorName6,
                NUDsectorName7, NUDsectorName8, NUDsectorName9};
            int[] sectorNameValues = NUD.collectNUD(NUDsectorNameValues);
            TextBox[] TBsectorTypes = {TBsectorType1, TBsectorType2,
                TBsectorType3, TBsectorType4, TBsectorType5, TBsectorType6,
                TBsectorType7, TBsectorType8, TBsectorType9};
            string[] sectorTypes = TB.collectTB(TBsectorTypes);
            NumericUpDown[] NUDsectorMinZones = {NUDsectorMinZones1, NUDsectorMinZones2,
                NUDsectorMinZones3, NUDsectorMinZones4, NUDsectorMinZones5, NUDsectorMinZones6,
                NUDsectorMinZones7, NUDsectorMinZones8, NUDsectorMinZones9};
            int[] sectorMinZones = NUD.collectNUD(NUDsectorMinZones);
            NumericUpDown[] NUDsectorMaxZones = {NUDsectorMaxZones1, NUDsectorMaxZones2,
                NUDsectorMaxZones3, NUDsectorMaxZones4, NUDsectorMaxZones5, NUDsectorMaxZones6,
                NUDsectorMaxZones7, NUDsectorMaxZones8, NUDsectorMaxZones9};
            int[] sectorMaxZones = NUD.collectNUD(NUDsectorMaxZones);
            NumericUpDown[] NUDsectorTypeValues = {NUDsectorType1, NUDsectorType2,
                NUDsectorType3, NUDsectorType4, NUDsectorType5, NUDsectorType6,
                NUDsectorType7, NUDsectorType8, NUDsectorType9};
            int[] sectorTypeValues = NUD.collectNUD(NUDsectorTypeValues);

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

            BTNzoneGenerate.Visible = true;
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
            
            NumericUpDown[] NUDbeforeSpawn = {NUDzoneBeforeSpawn1, NUDzoneBeforeSpawn2,
                NUDzoneBeforeSpawn3, NUDzoneBeforeSpawn4, NUDzoneBeforeSpawn5, NUDzoneBeforeSpawn6,
                NUDzoneBeforeSpawn7, NUDzoneBeforeSpawn8, NUDzoneBeforeSpawn9, NUDzoneBeforeSpawn10};
            int[] zoneEnemyBeforeSpawn = NUD.collectNUD(NUDbeforeSpawn);
            NumericUpDown[] NUDafterSpawn = {NUDzoneAfterSpawn1, NUDzoneAfterSpawn2,
                NUDzoneAfterSpawn3, NUDzoneAfterSpawn4, NUDzoneAfterSpawn5, NUDzoneAfterSpawn6,
                NUDzoneAfterSpawn7, NUDzoneAfterSpawn8, NUDzoneAfterSpawn9, NUDzoneAfterSpawn10};
            int[] zoneEnemyAfterSpawn = NUD.collectNUD(NUDafterSpawn);
            TextBox[] TBenemyNames = {TBzoneEnemy1, TBzoneEnemy2,
                TBzoneEnemy3, TBzoneEnemy4, TBzoneEnemy5, TBzoneEnemy6,
                TBzoneEnemy7, TBzoneEnemy8, TBzoneEnemy9, TBzoneEnemy10};
            string[] zoneEnemyNames = TB.collectTB(TBenemyNames);

            //если не отмечена ни одна сторона
            if (CLBzonesSides.CheckedItems.Count == 0)
            {
                LBLzoneInfo.Text = "Поставьте хоть 1 галочку \n в выбор сторон";
                return;
            }
            //инициализация нужных данных
            string enemy, enemies, shablonInfo, side;
            int shablonNum;
            int zonesNumber = Convert.ToInt32(NUDzoneCount.Value);
            zonesInfo = new string[zonesNumber];
            CBinfoZonesInfo.Items.Clear();
            //генерация зон
            zonesShablon = new int[zonesNumber];
            for (int i = 0; i < NUDzoneCount.Value; i++)
            {
                enemy = itemsGenerator(zoneEnemyNames, zoneEnemyBeforeSpawn, zoneEnemyAfterSpawn,
                    Convert.ToInt32(NUDzoneStartChance.Value), CBzoneCycled.Checked, CBzoneInterrupt.Checked);
                shablonNum = ran.Next(Convert.ToInt32(NUDzoneShablons.Value)) + 1;
                zonesShablon[i] = shablonNum;
 
                shablonInfo = "Шаблон №" + shablonNum.ToString() + "\nЗданий: " + shablon[shablonNum - 1] + "\n\n";
                side = "Перед смотрит на " + chooseSide() + "\n\n";
                enemies = "Враги в зоне: \n";
                if (enemy.Length == 0)
                    enemies += "Отсутствуют";
                else
                    enemies += enemy;
                //запись данных
                zonesInfo[i] = shablonInfo + side + enemies;
                CBinfoZonesInfo.Items.Add("Зона " + (i + 1).ToString());
            }
            

            //обновление подсказки 
            LBLzoneInfo.Text = "Теперь перейдите во вкладку Генератор Зданий \n\n Информацию о зонах можно посмотреть\n во вкладке Информация О Секторе";
            LBLzoneInfoGenerated.Text = $"Сгенерировано {NUDzoneCount.Value} Зон";

            BTNbuildingsGenerate.Visible = true;

        }

        //генератор врагов
        private string itemsGenerator(string[] items, int[] before, int[] after, int chance, bool cycled, bool interrupt)
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

        private void BTNreshuffle_Click(object sender, EventArgs e)
        {
            //сбор данных
            NumericUpDown[] NUDbeforeSpawn = {NUDzoneBeforeSpawn1, NUDzoneBeforeSpawn2,
                NUDzoneBeforeSpawn3, NUDzoneBeforeSpawn4, NUDzoneBeforeSpawn5, NUDzoneBeforeSpawn6,
                NUDzoneBeforeSpawn7, NUDzoneBeforeSpawn8, NUDzoneBeforeSpawn9, NUDzoneBeforeSpawn10};
            int[] zoneEnemyBeforeSpawn = NUD.collectNUD(NUDbeforeSpawn);
            NumericUpDown[] NUDafterSpawn = {NUDzoneAfterSpawn1, NUDzoneAfterSpawn2,
                NUDzoneAfterSpawn3, NUDzoneAfterSpawn4, NUDzoneAfterSpawn5, NUDzoneAfterSpawn6,
                NUDzoneAfterSpawn7, NUDzoneAfterSpawn8, NUDzoneAfterSpawn9, NUDzoneAfterSpawn10};
            int[] zoneEnemyAfterSpawn = NUD.collectNUD(NUDafterSpawn);
            TextBox[] TBenemyNames = {TBzoneEnemy1, TBzoneEnemy2,
                TBzoneEnemy3, TBzoneEnemy4, TBzoneEnemy5, TBzoneEnemy6,
                TBzoneEnemy7, TBzoneEnemy8, TBzoneEnemy9, TBzoneEnemy10};
            string[] zoneEnemyNames = TB.collectTB(TBenemyNames);
            //получение случайного порядка индексов
            int[] order = getOrder(zoneEnemyNames, zoneEnemyNames.Length);
            //присваиваем Textbox'ам и NUD'ам значения
            for(int i = 0; i <  order.Length; i++)
            {
                TBenemyNames[i].Text = zoneEnemyNames[order[i]];
                if(TBenemyNames[i].Text != "")
                {
                    NUDbeforeSpawn[i].Value = zoneEnemyBeforeSpawn[order[i]];
                    NUDafterSpawn[i].Value = zoneEnemyAfterSpawn[order[i]];
                }
                else //0,0 если пустой TextBox
                {
                    NUDbeforeSpawn[i].Value = 0;
                    NUDafterSpawn[i].Value = 0;
                }

                

            }


        }

        //получение случайного порядка индексов
        private int[] getOrder(string[] names, int len)
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
        private int[] delete(int[] mas, int ind)
        {
            int[] newMas = new int[mas.Length - 1];
            int j = 0;
            for(int i = 0; i < mas.Length; i++)
            {
                if (i == ind)
                    continue;
                newMas[j] = mas[i];
                j++;
            }
            return newMas;
        }
        //изменение кол-ва зон
        private void NUDzoneShablons_ValueChanged(object sender, EventArgs e)
        {
            CBzoneShablonSettings.Items.Clear(); //очищение ComboBox'а
            //добавление нужного числа шаблонов
            for (int i = 1; i <= NUDzoneShablons.Value; i++)
            {
                CBzoneShablonSettings.Items.Add("Шаблон №" + i.ToString());
            }
            //копирование настроек шаблонов
            int[] newShablon = new int[Convert.ToInt32(NUDzoneShablons.Value)];
            for(int i = 0; i < newShablon.Length; i++)
            {
                if (i >= shablon.Length)
                    newShablon[i] = 5;
                else
                    newShablon[i] = shablon[i];
            }
            shablon = newShablon; //новые настройки шаблонов
        }

        //изменение кол-ва зданий в шаблоне
        private void NUDzoneShablonSettings_ValueChanged(object sender, EventArgs e)
        {
            //сохраннение настройки для шаблона
            if(CBzoneShablonSettings.SelectedIndex != -1)
            {
                shablon[CBzoneShablonSettings.SelectedIndex] = Convert.ToInt32(NUDzoneShablonSettings.Value);
            }
        }

        //выбор другого шаблона
        private void CBshablonSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            NUDzoneShablonSettings.Value = shablon[CBzoneShablonSettings.SelectedIndex];
        }

        private void CBinfoZonesInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вывод информации о зоне
            LBLinfoZoneInfo.Text = zonesInfo[CBinfoZonesInfo.SelectedIndex];
            //вывод информации о зданиях
            LBLinfoBuildingsInfo1.Text = "";
            LBLinfoBuildingsInfo2.Text = "";
            string buildingInfo; //информация о конкретном здании
            for (int i = 0; i < buildingsInfo[CBinfoZonesInfo.SelectedIndex].Length; i++)
            {
                buildingInfo = buildingsInfo[CBinfoZonesInfo.SelectedIndex][i];
                if (i % 2 == 0)
                    LBLinfoBuildingsInfo1.Text += buildingInfo + "\n";
                else
                    LBLinfoBuildingsInfo2.Text += buildingInfo + "\n";
            }

        }

        private void BTNbuildingsGenerate_Click(object sender, EventArgs e)
        {
            TextBox[] TBbuildingTypes = {TBbuildingType1, TBbuildingType2,
                TBbuildingType3, TBbuildingType4, TBbuildingType5, TBbuildingType6,
                TBbuildingType7, TBbuildingType8, TBbuildingType9, TBbuildingType10};
            string[] buildingTypes = TB.collectTB(TBbuildingTypes);
            NumericUpDown[] NUDbuildingValues = {NUDbuildingType1, NUDbuildingType2,
                NUDbuildingType3, NUDbuildingType4, NUDbuildingType5, NUDbuildingType6,
                NUDbuildingType7, NUDbuildingType8, NUDbuildingType9, NUDbuildingType10};
            int[] buildingTypeValues = NUD.collectNUD(NUDbuildingValues);
            NumericUpDown[] NUDbuildingValueChanges = {NUDbuildingTypeChange1, NUDbuildingTypeChange2,
                NUDbuildingTypeChange3, NUDbuildingTypeChange4, NUDbuildingTypeChange5, NUDbuildingTypeChange6,
                NUDbuildingTypeChange7, NUDbuildingTypeChange8, NUDbuildingTypeChange9, NUDbuildingTypeChange10};
            int[] buildingTypeChanges = NUD.collectNUD(NUDbuildingValueChanges);
            NumericUpDown[] NUDbeforeSpawn = {NUDlootBeforeSpawn1, NUDlootBeforeSpawn2,
                NUDlootBeforeSpawn3, NUDlootBeforeSpawn4, NUDlootBeforeSpawn5, NUDlootBeforeSpawn6,
                NUDlootBeforeSpawn7, NUDlootBeforeSpawn8, NUDlootBeforeSpawn9, NUDlootBeforeSpawn10};
            int[] lootBeforeSpawn = NUD.collectNUD(NUDbeforeSpawn);
            NumericUpDown[] NUDafterSpawn = {NUDlootAfterSpawn1, NUDlootAfterSpawn2,
                NUDlootAfterSpawn3, NUDlootAfterSpawn4, NUDlootAfterSpawn5, NUDlootAfterSpawn6,
                NUDlootAfterSpawn7, NUDlootAfterSpawn8, NUDlootAfterSpawn9, NUDlootAfterSpawn10};
            int[] lootAfterSpawn = NUD.collectNUD(NUDafterSpawn);
            TextBox[] TBloot = {TBloot1, TBloot2,
                TBloot3, TBloot4, TBloot5, TBloot6,
                TBloot7, TBloot8, TBloot9, TBloot10};
            string[] lootNames = TB.collectTB(TBloot);


            string buildingInfo, buildingType, loot;
            int ind;
            buildingsInfo = new string[zonesInfo.Length][];
            string[] buildings;

            for(int i = 0; i<zonesShablon.Length; i++)
            {
                int buildingsCount = shablon[zonesShablon[i] - 1]; //количество зданий
                buildings = new string[buildingsCount];
                for(int j = 0; j<buildingsCount; j++)
                {

                    buildingInfo = "Здание №" + (j+1).ToString() + "\n\n";
                    buildingType = getName(buildingTypes, buildingTypeValues);

                    ind = findIndex(buildingTypes, buildingType);
                    buildingTypeValues[ind] -= buildingTypeChanges[ind];
                    if (buildingTypeValues[ind] < 1)
                        buildingTypeValues[ind] = 1;
                    loot = "Лут:\n" + itemsGenerator(lootNames, lootBeforeSpawn, lootAfterSpawn, 
                        Convert.ToInt32(NUDlootStartChance.Value), CBlootCycled.Checked, 
                        CBlootInterrupt.Checked);

                    buildingInfo += loot;

                    buildings[j] = buildingInfo;
                }

                buildingsInfo[i] = buildings;
            }

            LBLbuildingHint.Text = "Если вы следовали подсказкам,\nто теперь вы сможете посмотреть\nинформацию о секторе в следующей вкладке.";
        }

        private void BTNlootResuffle_Click(object sender, EventArgs e)
        {
            //сбор данных
            NumericUpDown[] NUDbeforeSpawn = {NUDlootBeforeSpawn1, NUDlootBeforeSpawn2,
                NUDlootBeforeSpawn3, NUDlootBeforeSpawn4, NUDlootBeforeSpawn5, NUDlootBeforeSpawn6,
                NUDlootBeforeSpawn7, NUDlootBeforeSpawn8, NUDlootBeforeSpawn9, NUDlootBeforeSpawn10};
            int[] lootBeforeSpawn = NUD.collectNUD(NUDbeforeSpawn);
            NumericUpDown[] NUDafterSpawn = {NUDlootAfterSpawn1, NUDlootAfterSpawn2,
                NUDlootAfterSpawn3, NUDlootAfterSpawn4, NUDlootAfterSpawn5, NUDlootAfterSpawn6,
                NUDlootAfterSpawn7, NUDlootAfterSpawn8, NUDlootAfterSpawn9, NUDlootAfterSpawn10};
            int[] lootAfterSpawn = NUD.collectNUD(NUDafterSpawn);
            TextBox[] TBloot = {TBloot1, TBloot2,
                TBloot3, TBloot4, TBloot5, TBloot6,
                TBloot7, TBloot8, TBloot9, TBloot10};
            string[] lootNames = TB.collectTB(TBloot);
            //получение случайного порядка индексов
            int[] order = getOrder(lootNames, lootNames.Length);
            //присваиваем Textbox'ам и NUD'ам значения
            for (int i = 0; i < order.Length; i++)
            {
                TBloot[i].Text = lootNames[order[i]];
                if (TBloot[i].Text != "")
                {
                    NUDbeforeSpawn[i].Value = lootBeforeSpawn[order[i]];
                    NUDafterSpawn[i].Value = lootAfterSpawn[order[i]];
                }
                else //0,0 если пустой TextBox
                {
                    NUDbeforeSpawn[i].Value = 0;
                    NUDafterSpawn[i].Value = 0;
                }



            }
        }
    }
}
