using English_Learning_Software1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class Result : UserControl
    {
        private int testcodez;
        private int ownercodez;
        private List<int> listz;
        private int sodung;
        EnglishDBContext context = new EnglishDBContext();
        public Result(int testcode,int ownercode,List<int> list,int dung)
        {
            InitializeComponent();
            testcodez = testcode;
            ownercodez = ownercode;
            listz = list;
            sodung = dung;
        }

   
        private void HienThiLenListView()
        {
            int index = 1;
            foreach(int item in listz)
            {
                ListViewItem lvi = new ListViewItem(index.ToString());
                if (item == 1)
                {
                    lvi.SubItems.Add("Đúng");
                    lvi.BackColor = Color.Green;
                }
                else
                {
                    lvi.SubItems.Add("Sai");
                    lvi.BackColor = Color.Red;
                }
                listView1.Items.Add(lvi);
                index++;
            }
        }
        private void Result_Load(object sender, EventArgs e)
        {
            Test a = context.Tests.FirstOrDefault(p => p.Test_Code == testcodez);
            if (a != null)
                label2.Text = a.Test_Name;
            Test_Detail b = context.Test_Detail.FirstOrDefault(p => p.Test_Code == testcodez&&p.Ownere_Code==ownercodez);
            if (b != null)
            {
                label3.Text = "Tổng số câu: "+listz.Count.ToString();
                label4.Text = "Số câu đúng: "+sodung.ToString();
                label5.Text= "Số câu sai: "+(listz.Count - sodung).ToString();
                label6.Text = "Điểm hiện tại: "+(sodung * 100) / listz.Count;
                label7.Text = "Điểm cao nhất: " + b.Test_Score;
            }
            HienThiLenListView();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Ownere a = context.Owneres.FirstOrDefault(p => p.Ownere_Code == ownercodez);
            Account b = context.Accounts.FirstOrDefault(p => p.Ownere_Code == a.Ownere_Code);
            frmHome.Instance.PnlContainer.Controls.Clear();
            ChooseTestz uc2 = new ChooseTestz(b.Account_Name);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
            frmHome.Instance.PnlContainer1.Enabled = true;
        }
    }
}
