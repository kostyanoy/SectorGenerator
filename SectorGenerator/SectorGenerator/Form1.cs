using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] sectorNames = collectTB(TBsectorName1, TBsectorName2, 
                TBsectorName3, TBsectorName4, TBsectorName5, TBsectorName6, 
                TBsectorName7, TBsectorName8, TBsectorName9);
            int[] sectorNameValues = collectNUD(NUDsectorName1, NUDsectorName2,
                NUDsectorName3, NUDsectorName4, NUDsectorName5, NUDsectorName6,
                NUDsectorName7, NUDsectorName8, NUDsectorName9);
            string[] sectorTypes = collectTB(TBsectorType1, TBsectorType2,
                TBsectorType3, TBsectorType4, TBsectorType5, TBsectorType6,
                TBsectorType7, TBsectorType8, TBsectorType9);
            int[] sectorMinZones = collectNUD(NUDsectorMinZones1, NUDsectorMinZones2,
                NUDsectorMinZones3, NUDsectorMinZones4, NUDsectorMinZones5, NUDsectorMinZones6,
                NUDsectorMinZones7, NUDsectorMinZones8, NUDsectorMinZones9);
            int[] sectorMaxZones = collectNUD(NUDsectorMaxZones1, NUDsectorMaxZones2,
                NUDsectorMaxZones3, NUDsectorMaxZones4, NUDsectorMaxZones5, NUDsectorMaxZones6,
                NUDsectorMaxZones7, NUDsectorMaxZones8, NUDsectorMaxZones9);
            int[] sectorTypeValues = collectNUD(NUDsectorType1, NUDsectorType2,
                NUDsectorType3, NUDsectorType4, NUDsectorType5, NUDsectorType6,
                NUDsectorType7, NUDsectorType8, NUDsectorType9);

            string result;
            int sectorTypeNum = 0;
            result = getName(sectorNames, sectorNameValues, out sectorTypeNum) + " ";
            
            string sectorType = getName(sectorTypes, sectorTypeValues, out sectorTypeNum);
            result += sectorType + " ";
            result += ran.Next(sectorMinZones[sectorTypeNum], sectorMaxZones[sectorTypeNum] + 1).ToString();
            result += " Зон";
            LBLresult.Text = result;
        }
        private string[] collectTB(params TextBox[] TBoxes)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                if (TBoxes[i].Text != "")
                    len++;
            }
            string[] names = new string[len];            
            for(int i = 0; i <9; i++)
            {
                if(TBoxes[i].Text != "")
                    names[i]= TBoxes[i].Text;                             
            }

            return names;
        }
        private int[] collectNUD(params NumericUpDown[] NUDs)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                if (NUDs[i].Value > 0)
                    len++;
            }

            int[] values = new int[len];
            for (int i = 0; i < 9; i++)
            {
                if(NUDs[i].Value > 0)
                    values[i] = Convert.ToInt32(NUDs[i].Value);
            }

            return values;
        }
        private string getName(string[] names, int[] values, out int sectorTypeNum)
        {
            int sum = 0;
            int maxNumber = 0;
            foreach(int value in values) { maxNumber += value; }
            int number = ran.Next(maxNumber);
            for(int i = 0; i < names.Length; i++)
            {
                sum += values[i];
                if (sum > number)
                {
                    sectorTypeNum = i;
                    return names[i];
                }
                    
            }
            sectorTypeNum = names.Length-1;
            return names[names.Length-1];
        }

    }
}
