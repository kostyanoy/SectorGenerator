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

        private void button1_Click(object sender, EventArgs e)
        {
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

            string result;           
            string sectorName = getName(sectorNames, sectorNameValues);            
            string sectorType = getName(sectorTypes, sectorTypeValues);
            int sectorTypeNum = findIndex(sectorTypes, sectorType);
            string Zones = ran.Next(sectorMinZones[sectorTypeNum], sectorMaxZones[sectorTypeNum] + 1).ToString();
            result = sectorName + " " + sectorType + " " + Zones + " Зон";
            LBLresult.Text = result;
        }        
        private int findIndex(string[] names, string name)
        {
            for(int i = 0; i < names.Length; i++)
            {
                if (names[i] == name)
                    return i;
            }
            return 0;
        }
        private string getName(string[] names, int[] values)
        {
            int sum = 0;
            int maxNumber = 0;
            foreach(int value in values) { maxNumber += value; }
            int number = ran.Next(maxNumber);
            for(int i = 0; i < names.Length; i++)
            {
                sum += values[i];
                if (sum > number)  
                    return names[i];  
            }
            return names[names.Length-1];
        }
        private string chooseSide()
        {
            if(CLBzonesSides.CheckedItems.Count > 0)
            {
                int i = ran.Next(CLBzonesSides.CheckedItems.Count);
                return CLBzonesSides.CheckedItems[i].ToString();
            }
            return "Стороны не выбраны";
                      
        }
        private bool chance100(int chance)
        {
            return chance > ran.Next(100);
        }

        private void BTNzoneCounter_Click(object sender, EventArgs e)
        {
            counter++;
            LBLzoneCounter.Text = counter.ToString();
        }

        private void BTNzoneResult_Click(object sender, EventArgs e)
        {
            
            int[] zoneEnemyValues = NUD.collectNUD(NUDzoneEnemy1, NUDzoneEnemy2, 
                NUDzoneEnemy3, NUDzoneEnemy4, NUDzoneEnemy5, NUDzoneEnemy6, 
                NUDzoneEnemy7, NUDzoneEnemy8, NUDzoneEnemy9, NUDzoneEnemy10);
            string[] zoneEnemyNames = TB.collectTB(TBzoneEnemy1, TBzoneEnemy2, 
                TBzoneEnemy3, TBzoneEnemy4, TBzoneEnemy5, TBzoneEnemy6, 
                TBzoneEnemy7, TBzoneEnemy8, TBzoneEnemy9, TBzoneEnemy10);
            
            string result = "Шаблон №" + (ran.Next(Convert.ToInt32(NUDzoneTypes.Value)) + 1).ToString() + "\n";
            string side = chooseSide();
            result += "Перед смотрит на " + side + "\n";
            LBLzoneResult.Text = result;

            result = "Враги в зоне: \n";
            string enemies = "";
            for(int i = 0; i < zoneEnemyNames.Length; i++)
            {
                if (chance100(zoneEnemyValues[i]))
                    enemies += zoneEnemyNames[i] + "\n";
            }
            if (enemies == "")
                enemies = "Отсутствуют";
            result += enemies;
            

            LBLzoneEnemies.Text = result;
        }
    }
}
