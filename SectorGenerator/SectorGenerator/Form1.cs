using SectorGenerator.Properties;
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

        string[] successes = { "Успех!", "Да!", "Успех", "Успешно", "=)", "Yes", "Удачно", "Удача" }; //удачи
        string[] fails = { "Неа", "Нет", "Провал", "=(", "Тотальный провал", "Неудача", "NIEN!!!", "Nicht", "Неее"}; //неудачи

        int counter = 0; //счётчик нажатий на кнопку

        //настройки
        SectorDangerLevel[] levelSettings = new SectorDangerLevel[1] { new SectorDangerLevel() };

        //секторы 
        Sector selectedSector = new Sector("Сектор №1");
        int sectorCounter = 1; //номер сектора
        List<Sector> sectorsInfo = new List<Sector>();

        int level = 0;
        int zones = 0;
        int specInd = 0;

        SectorNamesSettings sectorNamesSettingsCopied = new SectorNamesSettings();
        SectorTypesSettings sectorTypesSettingsCopied = new SectorTypesSettings();

        //здания
        BuildingSettings buildingsSettingsCopied = new BuildingSettings();

        //==========================
        //массивы для конструктора
        //специализации
        TextBox[] TBsectorSpecs;
        string[] sectorSpecs;
        NumericUpDown[] NUDsectorSpecsValues;
        int[] sectorSpecsValues;
        //типы
        TextBox[] TBsectorTypes;
        string[] sectorTypes;
        NumericUpDown[] NUDsectorMinZones;
        int[] sectorMinZones;
        NumericUpDown[] NUDsectorMaxZones;
        int[] sectorMaxZones;
        NumericUpDown[] NUDsectorTypeValues;
        int[] sectorTypeValues;
        //зоны
        NumericUpDown[] NUDzoneBeforeSpawn;
        int[] zoneEnemyBeforeSpawn;
        NumericUpDown[] NUDzoneAfterSpawn;
        int[] zoneEnemyAfterSpawn;
        TextBox[] TBenemyNames;
        string[] zoneEnemyNames;
        //здания
        //типы
        TextBox[] TBbuildingTypes;
        string[] buildingTypes;
        NumericUpDown[] NUDbuildingValues;
        int[] buildingTypeValues;
        NumericUpDown[] NUDbuildingValueChanges;
        int[] buildingTypeChanges;
        //лут
        NumericUpDown[] NUDlootBeforeSpawn;
        int[] lootBeforeSpawn;
        NumericUpDown[] NUDlootAfterSpawn;
        int[] lootAfterSpawn;
        TextBox[] TBloot;
        string[] lootNames;




        public Form1()
        {
            InitializeComponent();
            //секторы
            sectorsInfo.Add(selectedSector);
            CBinfoSectorChoose.Items.Add(selectedSector.Name);
            buildingsSaveSettings();
            for(int i = 0; i < 4; i++)
            {
                CLBzonesSides.SetItemChecked(i, true);
            }


            init();
        }

        private void init()
        {
            //специализации
            TBsectorSpecs = new TextBox[] { TBsectorName1, TBsectorName2,
                TBsectorName3, TBsectorName4, TBsectorName5, TBsectorName6,
                TBsectorName7, TBsectorName8, TBsectorName9};
            sectorSpecs = TB.collectTB(TBsectorSpecs);
            NUDsectorSpecsValues = new NumericUpDown[] {NUDsectorName1, NUDsectorName2,
                NUDsectorName3, NUDsectorName4, NUDsectorName5, NUDsectorName6,
                NUDsectorName7, NUDsectorName8, NUDsectorName9};
            sectorSpecsValues = NUD.collectNUD(NUDsectorSpecsValues);
            //типы
            TBsectorTypes = new TextBox[] {TBsectorType1, TBsectorType2,
                TBsectorType3, TBsectorType4, TBsectorType5, TBsectorType6,
                TBsectorType7, TBsectorType8, TBsectorType9};
            sectorTypes = TB.collectTB(TBsectorTypes);
            NUDsectorMinZones = new NumericUpDown[] {NUDsectorMinZones1, NUDsectorMinZones2,
                NUDsectorMinZones3, NUDsectorMinZones4, NUDsectorMinZones5, NUDsectorMinZones6,
                NUDsectorMinZones7, NUDsectorMinZones8, NUDsectorMinZones9};
            sectorMinZones = NUD.collectNUD(NUDsectorMinZones);
            NUDsectorMaxZones = new NumericUpDown[] {NUDsectorMaxZones1, NUDsectorMaxZones2,
                NUDsectorMaxZones3, NUDsectorMaxZones4, NUDsectorMaxZones5, NUDsectorMaxZones6,
                NUDsectorMaxZones7, NUDsectorMaxZones8, NUDsectorMaxZones9};
            sectorMaxZones = NUD.collectNUD(NUDsectorMaxZones);
            NUDsectorTypeValues = new NumericUpDown[] {NUDsectorType1, NUDsectorType2,
                NUDsectorType3, NUDsectorType4, NUDsectorType5, NUDsectorType6,
                NUDsectorType7, NUDsectorType8, NUDsectorType9};
            sectorTypeValues = NUD.collectNUD(NUDsectorTypeValues);
            //зоны
            NUDzoneBeforeSpawn = new NumericUpDown[] {NUDzoneBeforeSpawn1, NUDzoneBeforeSpawn2,
                NUDzoneBeforeSpawn3, NUDzoneBeforeSpawn4, NUDzoneBeforeSpawn5, NUDzoneBeforeSpawn6,
                NUDzoneBeforeSpawn7, NUDzoneBeforeSpawn8, NUDzoneBeforeSpawn9, NUDzoneBeforeSpawn10};
            zoneEnemyBeforeSpawn = NUD.collectNUD(NUDzoneBeforeSpawn);
            NUDzoneAfterSpawn = new NumericUpDown[] {NUDzoneAfterSpawn1, NUDzoneAfterSpawn2,
                NUDzoneAfterSpawn3, NUDzoneAfterSpawn4, NUDzoneAfterSpawn5, NUDzoneAfterSpawn6,
                NUDzoneAfterSpawn7, NUDzoneAfterSpawn8, NUDzoneAfterSpawn9, NUDzoneAfterSpawn10};
            zoneEnemyAfterSpawn = NUD.collectNUD(NUDzoneAfterSpawn);
            TBenemyNames = new TextBox[] {TBzoneEnemy1, TBzoneEnemy2,
                TBzoneEnemy3, TBzoneEnemy4, TBzoneEnemy5, TBzoneEnemy6,
                TBzoneEnemy7, TBzoneEnemy8, TBzoneEnemy9, TBzoneEnemy10};
            zoneEnemyNames = TB.collectTB(TBenemyNames);
            //здания
            //типы
            TBbuildingTypes = new TextBox[] {TBbuildingType1, TBbuildingType2,
                TBbuildingType3, TBbuildingType4, TBbuildingType5, TBbuildingType6,
                TBbuildingType7, TBbuildingType8, TBbuildingType9, TBbuildingType10};
            buildingTypes = TB.collectTB(TBbuildingTypes);
            NUDbuildingValues = new NumericUpDown[] {NUDbuildingType1, NUDbuildingType2,
                NUDbuildingType3, NUDbuildingType4, NUDbuildingType5, NUDbuildingType6,
                NUDbuildingType7, NUDbuildingType8, NUDbuildingType9, NUDbuildingType10};
            buildingTypeValues = NUD.collectNUD(NUDbuildingValues);
            NUDbuildingValueChanges = new NumericUpDown[] {NUDbuildingTypeChange1, NUDbuildingTypeChange2,
                NUDbuildingTypeChange3, NUDbuildingTypeChange4, NUDbuildingTypeChange5, NUDbuildingTypeChange6,
                NUDbuildingTypeChange7, NUDbuildingTypeChange8, NUDbuildingTypeChange9, NUDbuildingTypeChange10};
            buildingTypeChanges = NUD.collectNUD(NUDbuildingValueChanges);
            //лут
            NUDlootBeforeSpawn = new NumericUpDown[] {NUDlootBeforeSpawn1, NUDlootBeforeSpawn2,
                NUDlootBeforeSpawn3, NUDlootBeforeSpawn4, NUDlootBeforeSpawn5, NUDlootBeforeSpawn6,
                NUDlootBeforeSpawn7, NUDlootBeforeSpawn8, NUDlootBeforeSpawn9, NUDlootBeforeSpawn10};
            lootBeforeSpawn = NUD.collectNUD(NUDlootBeforeSpawn);
            NUDlootAfterSpawn = new NumericUpDown[] {NUDlootAfterSpawn1, NUDlootAfterSpawn2,
                NUDlootAfterSpawn3, NUDlootAfterSpawn4, NUDlootAfterSpawn5, NUDlootAfterSpawn6,
                NUDlootAfterSpawn7, NUDlootAfterSpawn8, NUDlootAfterSpawn9, NUDlootAfterSpawn10};
            lootAfterSpawn = NUD.collectNUD(NUDlootAfterSpawn);
            TBloot = new TextBox[] {TBloot1, TBloot2,
                TBloot3, TBloot4, TBloot5, TBloot6,
                TBloot7, TBloot8, TBloot9, TBloot10};
            lootNames = TB.collectTB(TBloot);
        }

        //генерация сектора
        private void button1_Click(object sender, EventArgs e)
        {
            string result, spec, info, type; 
            //выбор спецификации
            level = Convert.ToInt32(NUDsectorDangerLevel.Value) - 1;
            spec = levelSettings[level].NamesSettings.Choose();
            if (spec == "") //если не выбрана спецификация сектора
            {
                LBLsectorHint.Text = "Введите возможные спецификации сектора и укажите их частоту";
                return; 
            }
            info = "Спецификация:\n" + spec + "\n\n";
            //выбор типа
            specInd = levelSettings[level].NamesSettings.ChoosedIndex;
            type = levelSettings[level].SpecSettings[specInd].TypesSettings.Choose();
            if (type == "") //если не выбран тип сектора
            {
                LBLsectorHint.Text = "Введите возможные типы сектора и укажите их частоту";
                return; 
            }
            info += "Тип:\n" + type + "\n\n";
            //количество зон
            zones = levelSettings[level].SpecSettings[specInd].TypesSettings.Zones;

            info += "Количество зон:\n" + zones + "\n\n";
            result = spec + " " + type + " " + zones + " Зон";

            //запись данных
            LBLsectorResult.Text = result;
            LBLsectorHint.Text = "Теперь перейдите во вкладку Генератор Зон \n\n Информацию о секторе можно посмотреть\n во вкладке Информация О Секторе ";

            LBLinfoSectorInfo.Text = info;

            BTNzoneGenerate.Visible = true;

            //сохраниение информации о секторе
            selectedSector.Spec = spec;
            selectedSector.Name = TBsectorMainNameChange.Text;
            selectedSector.Zones = new Zone[zones];

            selectedSector.Info = info;

        }       
        
        //поиск индекса в массиве
        public int findIndex(string[] names, string name)
        {
            for(int i = 0; i < names.Length; i++)
            {
                if (names[i] == name)
                    return i;
            }
            //если строки не было в массиве
            return 0; 
        }
                
        //генерация зоны
        private void BTNzoneResult_Click(object sender, EventArgs e)
        {
            //если не отмечена ни одна сторона
            if (CLBzonesSides.CheckedItems.Count == 0)
            {
                LBLzoneInfo.Text = "Поставьте хоть 1 галочку \n в выбор сторон";
                return;
            }
            CBinfoZonesInfo.Items.Clear();
            //генерация зон
            selectedSector.Zones = levelSettings[level].SpecSettings[specInd].Generate(zones);
            for (int i = 0; i < zones; i++)
            {                
                CBinfoZonesInfo.Items.Add(selectedSector.Zones[i].Name);
            }
            

            //обновление подсказки 
            LBLzoneInfo.Text = "Теперь перейдите во вкладку Генератор Зданий \n\n Информацию о зонах можно посмотреть\n во вкладке Информация О Секторе";
            LBLzoneInfoGenerated.Text = $"Сгенерировано {selectedSector.Zones.Length} Зон";

            BTNbuildingsGenerate.Visible = true;

            LBLinfoBuildingsInfo1.Text = "Отсутствует";
            LBLinfoBuildingsInfo2.Text = "";
        }

        //генератор зданий и лута
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

        //нажатие на кнопку "Если скучно"
        private void BTNzoneCounter_Click(object sender, EventArgs e)
        {
            counter++;
            LBLzoneCounter.Text = counter.ToString();//добавление единицы
        }

        //перемешивание 
        private void BTNreshuffle_Click(object sender, EventArgs e)
        {
            levelSettings[level].SpecSettings[specInd].ReshuffleNames();
            levelSettings[level].SpecSettings[specInd].Input(TBenemyNames, NUDzoneBeforeSpawn, NUDzoneAfterSpawn,NUDzoneStartChance, CBzonesReshuffleEveryGen, CBzoneCycled, CBzoneInterrupt, CLBzonesSides);

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

        //изменение кол-ва шаблонов
        private void NUDzoneShablons_ValueChanged(object sender, EventArgs e)
        {
            shablonUpdate();
            CBzoneShablonSettings.SelectedIndex = Convert.ToInt32(NUDzoneShablons.Value) - 1;
        }

        private void shablonUpdate()
        {
            Shablon[] newShablonsInfo = new Shablon[Convert.ToInt32(NUDzoneShablons.Value)];
            CBzoneShablonSettings.Items.Clear(); //очищение ComboBox'а
            //добавление и копирование нужного числа шаблонов
            for (int i = 1; i <= NUDzoneShablons.Value; i++)
            {
                if (i <= shablonsInfo.Length)
                {
                    CBzoneShablonSettings.Items.Add(shablonsInfo[i - 1].Name);
                    newShablonsInfo[i - 1] = shablonsInfo[i - 1];
                }
                else
                {
                    CBzoneShablonSettings.Items.Add("Шаблон №" + i.ToString());
                    newShablonsInfo[i - 1] = new Shablon("Шаблон №" + i.ToString(), 5);
                }
            }
            shablonsInfo = newShablonsInfo; //новые настройки шаблонов
        }

        //изменение кол-ва зданий в шаблоне
        private void NUDzoneShablonSettings_ValueChanged(object sender, EventArgs e)
        {
            //сохраннение настройки для шаблона
            if (CBzoneShablonSettings.SelectedIndex == -1)
            {
                if (CBzoneShablonSettings.Items.Count > 0)
                    CBzoneShablonSettings.SelectedIndex = 0;
                else
                    return;
            }
            shablonsInfo[CBzoneShablonSettings.SelectedIndex].Buildings = Convert.ToInt32(NUDzoneShablonSettings.Value);
            
        }

        //выбор другого шаблона
        private void CBshablonSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBzoneShablonSettings.SelectedIndex == -1)
            {
                if (CBzoneShablonSettings.Items.Count > 0)
                    CBzoneShablonSettings.SelectedIndex = 0;
                else
                    return;
            }
            int ind = CBzoneShablonSettings.SelectedIndex;
            NUDzoneShablonSettings.Value = shablonsInfo[ind].Buildings;
            TBzoneShablonNameChange.Text = shablonsInfo[CBzoneShablonSettings.SelectedIndex].Name;
 

        }

        //изменение выбора зоны
        private void CBinfoZonesInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вывод информации о зоне
            int ind = CBinfoZonesInfo.SelectedIndex;
            LBLinfoZoneInfo.Text = selectedSector.Zones[ind].Info;
            //вывод информации о зданиях
            LBLinfoBuildingsInfo1.Text = "";
            LBLinfoBuildingsInfo2.Text = "";
            string buildingInfo; //информация о конкретном здании
            for (int i = 0; i < selectedSector.Zones[ind].Buildings.Length; i++)
            {
                if (selectedSector.Zones[ind].Buildings[i] != null)
                {
                    buildingInfo = selectedSector.Zones[ind].Buildings[i].Info;
                    if (i % 2 == 0) //в первый столбец
                        LBLinfoBuildingsInfo1.Text += buildingInfo + "\n\n";
                    else //во второй столбец
                        LBLinfoBuildingsInfo2.Text += buildingInfo + "\n\n";
                }
            }

        }

        //создание зданий
        private void BTNbuildingsGenerate_Click(object sender, EventArgs e)
        {
            //сбор данных при генерации зданий
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

            buildingsSaveSettings();

            //инициализация переменных
            string buildingInfo, buildingType, loot;
            int ind;
            int buildingsCount;
            //создание зданий по шаблону
            for (int i = 0; i<selectedSector.Zones.Length; i++)
            {
                buildingsCount = shablonsInfo[selectedSector.Zones[i].Shablon - 1].Buildings; //количество зданий
                selectedSector.Zones[i].Buildings = new Building[buildingsCount]; //здания в конкретной зоне
                //создание зданий
                for(int j = 0; j<buildingsCount; j++)
                {
                    selectedSector.Zones[i].Buildings[j] = new Building();
                    buildingInfo = "Здание №" + (j+1).ToString() + "\n"; //номер здания
                    buildingType = TB.getName(buildingTypes, buildingTypeValues); //тип здания

                    ind = findIndex(buildingTypes, buildingType); 
                    //изменение частоты зданий
                    buildingTypeValues[ind] -= buildingTypeChanges[ind];
                    if (buildingTypeValues[ind] < 1)
                        buildingTypeValues[ind] = 1;
                    //создание лута
                    loot = "Лут:\n" + itemsGenerator(lootNames, lootBeforeSpawn, lootAfterSpawn, 
                        Convert.ToInt32(NUDlootStartChance.Value), CBlootCycled.Checked, 
                        CBlootInterrupt.Checked);

                    //сохраниение сгенерированных переменных
                    buildingInfo += buildingType + "\n========\n";
                    buildingInfo += loot;

                    selectedSector.Zones[i].Buildings[j].Info = buildingInfo;
                }
            }

            //обновление поздсказки
            LBLbuildingHint.Text = "Если вы следовали подсказкам,\nто теперь вы сможете посмотреть\nинформацию о секторе в следующей вкладке.";

            //сразу выбор зоны
            if (CBinfoZonesInfo.Items.Count > 0)
                CBinfoZonesInfo.SelectedIndex = 0;
        }

        //кнопка перемешивания лута
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
        
        //изменение названия текущего сектора
        private void BTNsectorMainNameChange_Click(object sender, EventArgs e)
        {
            string name = TBsectorMainNameChange.Text;
            LBLsectorMainName.Text = name;
            selectedSector.Name = name;
            CBinfoSectorChoose.Items.Clear();
            foreach(Sector sec in sectorsInfo)
            {
                CBinfoSectorChoose.Items.Add(sec.Name);
            }
            CBinfoSectorChoose.Text = selectedSector.Name;
        }

        //создать новый сектор
        private void BTNcreateNewSector_Click(object sender, EventArgs e)
        {
            selectedSector = new Sector("Сектор №" + (++sectorCounter).ToString()); //создание объекта
            sectorsInfo.Add(selectedSector); //добавление объекта в список
            //изменение выбора сектора
            CBinfoSectorChoose.Items.Add(selectedSector.Name); 
            CBinfoSectorChoose.Text = selectedSector.Name;

            TBsectorMainNameChange.Text = selectedSector.Name; //изменение в TextBox'е

            //удаление зон
            CBinfoZonesInfo.Items.Clear();
            CBinfoZonesInfo.Text = "";

            //обновление надписей
            LBLsectorMainName.Text = selectedSector.Name; //изменение имени сектора
            LBLinfoSectorInfo.Text = "Отсутствует";
            LBLinfoZoneInfo.Text = "Отсутствует";
            LBLinfoBuildingsInfo1.Text = "Отсутствует";
            LBLinfoBuildingsInfo2.Text = "";
            
        }

        //выбор сектора из созданных
        private void CBinfoSectorChoose_SelectedIndexChanged(object sender, EventArgs e)
        {       
            //обновнение данных о текущей зоне
            selectedSector = sectorsInfo[CBinfoSectorChoose.SelectedIndex]; //выбор нужной зоны из списка
            CBinfoZonesInfo.Text = "";
            LBLinfoSectorInfo.Text = selectedSector.Info; //изменение информации о секторе

            //изменение выбора зон
            CBinfoZonesInfo.Items.Clear();
            foreach(Zone zone in selectedSector.Zones)
            {
                if(zone != null)
                    CBinfoZonesInfo.Items.Add(zone.Name);
            }

            TBsectorMainNameChange.Text = selectedSector.Name; //изменение в TextBox'е

            //изменение надписей
            LBLinfoSectorInfo.Text = selectedSector.Info; //изменение информации о секторе
            LBLinfoBuildingsInfo1.Text = "Отсутствует";
            LBLinfoBuildingsInfo2.Text = "";
            LBLinfoZoneInfo.Text = "Отсутствует";
            LBLsectorMainName.Text = selectedSector.Name;

        }
        //сгененрировать число в диапазоне
        private void BTNrandomazerDiapazoneGenerate_Click(object sender, EventArgs e)
        {
            int from = Convert.ToInt32(NUDrandomazerDiapazonFrom.Value);
            int to = Convert.ToInt32(NUDrandomazerDiapazonTo.Value);
            if (from > to)
            {
                int t = from;
                from = to;
                to = t;
            }
            LBLrandomazerDiapazoneResult.Text = Convert.ToString(ran.Next(from, to+1));
        }
        //проверка на удачу
        private void BTNrandomazerChanceGenerate_Click(object sender, EventArgs e)
        {
            int line = Convert.ToInt32(NUDrandomazerChance.Value);
            if(ran.Next(1, 101) <= line)
                LBLrandomazerChanceResult.Text = chooseMassive(successes);           
            else
                LBLrandomazerChanceResult.Text = chooseMassive(fails);
            
        }
        //выбор элемента из массива
        private string chooseMassive(string[] mas)
        {
            string res = mas[ran.Next(mas.Length)];
            return res;
        }
        //бросок множества кубиков за раз
        private void BTNrandomazerDicesGenerate_Click(object sender, EventArgs e)
        {
            if (TBrandomazerDices.Text == "")
                return;
            string[] st = TBrandomazerDices.Text.Split(',');
            int[] dices = new int[st.Length];
            for(int i = 0; i<st.Length; i++)
            {
                if(st[i] != "")
                    dices[i] = Convert.ToInt32(st[i].Trim());
            }
            LBLrandomazerDicesResult.Text = "";
            foreach(int dice in dices)
            {
                    LBLrandomazerDicesResult.Text += (ran.Next(dice) + 1).ToString() + " ";
            }

            
        }
        //изменение имени выбранного шаблона
        private void BTNshablonNameChange_Click(object sender, EventArgs e)
        {
            if (CBzoneShablonSettings.SelectedIndex == -1)
            {
                if (CBzoneShablonSettings.Items.Count > 0)
                    CBzoneShablonSettings.SelectedIndex = 0;
                else
                    return;
            }
            string name = TBzoneShablonNameChange.Text;
            shablonsInfo[CBzoneShablonSettings.SelectedIndex].Name = name;
            int ind = CBzoneShablonSettings.SelectedIndex; 
            shablonUpdate();
            CBzoneShablonSettings.SelectedIndex = ind;
        }
        //собрать наствройки лута в здании
        private BuildingSettings buildingCollectSettings(string name)
        {
            BuildingSettings set = new BuildingSettings(); //создание экземпляра класса
            set.Name = name;
            set.Interrupt = CBlootInterrupt.Checked;
            set.Cycled = CBlootCycled.Checked;
            set.Reshuffle = CBlootReshuffleEveryGen.Checked;
            set.StaringChance = Convert.ToInt32(NUDlootStartChance.Value);

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

            //отсеивание пустых TextBox'ов
            int count = 0;
            List<int> inds = new List<int>();
            for (int i = 0; i < TBloot.Length; i++)
            {
                if (TBloot[i].Text == "")
                    continue;
                count++;
                inds.Add(i);
            }
            //сохранение введёных данных
            set.LootNames = new string[count];
            set.LootBeforeSpawn = new int[count];
            set.LootAfterSpawn = new int[count];
            for(int i = 0; i < count; i++)
            {
                set.LootNames[i] = lootNames[inds[i]];
                set.LootBeforeSpawn[i] = lootBeforeSpawn[inds[i]];
                set.LootAfterSpawn[i] = lootAfterSpawn[inds[i]];
            }
            set.Cycled = CBlootCycled.Checked;
            set.Interrupt = CBlootInterrupt.Checked;
            set.Reshuffle = CBlootReshuffleEveryGen.Checked;
            set.StaringChance = Convert.ToInt32(NUDlootStartChance.Value);


            return set;
        }

      
        //изменение выбора настройки тпиа здания
        private void CBbuildingsTypeChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildingsInputSettings(buildingTypeChoose.BuildingSettings[CBbuildingsTypeChoose.SelectedIndex]);
                    
        }

        //сохранение настроек лута в зданиях
        private void buildingsSaveSettings()
        {
            int ind = CBbuildingsTypeChoose.SelectedIndex;
            CBbuildingsTypeChoose.Items.Clear();
            BuildingTypesSettings newBTC = new BuildingTypesSettings();
            //сбор данных
            TextBox[] TBbuildingTypes = {TBbuildingType1, TBbuildingType2,
                TBbuildingType3, TBbuildingType4, TBbuildingType5, TBbuildingType6,
                TBbuildingType7, TBbuildingType8, TBbuildingType9, TBbuildingType10};
            string[] buildingTypes = TB.collectTB(TBbuildingTypes);
            //отсеивание пустых
            int count = 0;
            List<int> inds = new List<int>();
            string name;
            bool old = false;
            for (int i = 0; i < buildingTypes.Length; i++)
            {
                if (buildingTypes[i] == "")
                    continue;
                count++;
                inds.Add(i);
            }
            newBTC.BuildingSettings = new BuildingSettings[count];
            for (int i = 0; i < count; i++)
            {
                name = buildingTypes[inds[i]];
                old = false;
                foreach(BuildingSettings t in buildingTypeChoose.BuildingSettings)
                {
                    if (t.Name == name)
                        old = true;
                }
                CBbuildingsTypeChoose.Items.Add(name);
                if (i == ind)
                {
                    newBTC.BuildingSettings[i] = buildingCollectSettings(name);
                    continue;
                }
                else if(old)
                {
                    newBTC.BuildingSettings[i] = buildingTypeChoose.BuildingSettings[i];
                    continue;
                }
                newBTC.BuildingSettings[i] = buildingCollectSettings(name);

            }
            buildingTypeChoose = newBTC;
        }
        //загрузка настроек лута в зданиях
        private void buildingsInputSettings(BuildingSettings bS)
        {
            NumericUpDown[] NUDbeforeSpawn = {NUDlootBeforeSpawn1, NUDlootBeforeSpawn2,
                NUDlootBeforeSpawn3, NUDlootBeforeSpawn4, NUDlootBeforeSpawn5, NUDlootBeforeSpawn6,
                NUDlootBeforeSpawn7, NUDlootBeforeSpawn8, NUDlootBeforeSpawn9, NUDlootBeforeSpawn10};
            NumericUpDown[] NUDafterSpawn = {NUDlootAfterSpawn1, NUDlootAfterSpawn2,
                NUDlootAfterSpawn3, NUDlootAfterSpawn4, NUDlootAfterSpawn5, NUDlootAfterSpawn6,
                NUDlootAfterSpawn7, NUDlootAfterSpawn8, NUDlootAfterSpawn9, NUDlootAfterSpawn10};
            TextBox[] TBloot = {TBloot1, TBloot2,
                TBloot3, TBloot4, TBloot5, TBloot6,
                TBloot7, TBloot8, TBloot9, TBloot10};
            for (int i = 0; i < TBloot.Length; i++)
            {
                if (i < bS.LootNames.Length)
                {
                    TBloot[i].Text = bS.LootNames[i];
                    NUDafterSpawn[i].Value = bS.LootAfterSpawn[i];
                    NUDbeforeSpawn[i].Value = bS.LootBeforeSpawn[i];
                }
                else
                {
                    TBloot[i].Text = "";
                    NUDafterSpawn[i].Value = 0;
                    NUDbeforeSpawn[i].Value = 0;
                }

                CBlootCycled.Checked = bS.Cycled;
                CBlootInterrupt.Checked = bS.Interrupt;
                CBlootReshuffleEveryGen.Checked = bS.Reshuffle;
                NUDlootStartChance.Value = bS.StaringChance;
            }
        }
        //нажатие на выбор настрое к типа здания
        private void CBbuildingsTypeChoose_Click(object sender, EventArgs e)
        {
            buildingsSaveSettings();
        }
        //нажатие на кнопку копироварния настроек типа здания
        private void BTNbuildingsCopySettings_Click(object sender, EventArgs e)
        {
            buildingsSettingsCopied = buildingCollectSettings("");
        }
        //нажатие на кнопку вставки настроек типа здания
        private void BTNbuildingPasteSettings_Click(object sender, EventArgs e)
        {
            buildingsInputSettings(buildingsSettingsCopied);
        }

        private void BTNsectorNamesCopy_Click(object sender, EventArgs e)
        {
            sectorNamesSettingsCopied = sectorNamesCollectSettings();
        }

        private void BTNsectorNamesPaste_Click(object sender, EventArgs e)
        {
            sectorNamesSettingsCopied.Input();
        }

        private void BTNsectorTypesCopy_Click(object sender, EventArgs e)
        {
            sectorTypesSettingsCopied = sectorTypesCollectSettings();
        }

        private void BTNsectorTypesPaste_Click(object sender, EventArgs e)
        {
            sectorTypesInputSettings(sectorTypesSettingsCopied);
        }

        private SectorNamesSettings sectorNamesCollectSettings()
        {
            SectorNamesSettings set = new SectorNamesSettings();
            set.Co
            return set;
        }
    }
}
