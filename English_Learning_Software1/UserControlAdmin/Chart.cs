using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English_Learning_Software1.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace English_Learning_Software1.UserControlAdmin
{
    public partial class Chart : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        string[] xeploai = new string[4];
        double[] tile = new double[4];
        public Chart()
        {
            InitializeComponent();
            Them();
        }
        private int XepLoai(Test_Detail test_Detail)
        {
            if(80<=test_Detail.Test_Score&& test_Detail.Test_Score <= 100)
            {
                return 1;
            }else if(70 <= test_Detail.Test_Score && test_Detail.Test_Score < 80)
            {
                return 2;
            }else if(50 <= test_Detail.Test_Score && test_Detail.Test_Score < 70)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        int d1=0, d2=0, d3=0, d4=0;
        private void Them()
        {
            List<Test_Detail> test_Details = context.Test_Detail.ToList();
            foreach(var item in test_Details)
            {
                if (XepLoai(item) == 1)
                {
                    d1++;
                }
                else if(XepLoai(item)==2)
                {
                    d2++;
                }
                else if (XepLoai(item) == 3)
                {
                    d3++;
                }
                else
                {
                    d4++;
                }
            }
            tile[0] = (d1*100) / (test_Details.Count);
            tile[1] = (d2 * 100) / (test_Details.Count);
            tile[2] = (d3 * 100) / (test_Details.Count);
            tile[3] = (d4 * 100) / (test_Details.Count);
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].Points.AddXY("A(80-100)", tile[0]);
            chart1.Series["Series1"].Points.AddXY("B(60-79)", tile[1]);
            chart1.Series["Series1"].Points.AddXY("C(50-59)", tile[2]);
            chart1.Series["Series1"].Points.AddXY("D(<50)", tile[3]);
        }
    }
}
