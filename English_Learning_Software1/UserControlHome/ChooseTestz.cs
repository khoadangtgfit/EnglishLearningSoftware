using English_Learning_Software1.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class ChooseTestz : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        private string owner;
        public ChooseTestz(string username)
        {
            InitializeComponent();
            this.owner = username;
            LoadTest();
        }
        private void LoadTest()
        {
            List<Test> listtest = context.Tests.ToList();
            foreach (Test item in listtest)
            {
                Guna2Button a = new Guna2Button();
                a.FillColor = Color.FromArgb(91, 210, 240);
                a.Font = new Font(FontFamily.GenericSansSerif, 10.0F);
                a.BorderRadius = 8;
                a.Height = 45;
                a.Width = 180;
                a.Text = item.Test_Name;
                a.Dock = DockStyle.Top;
                a.Click += A_Click;
                flowLayoutPanel1.Controls.Add(a);
            }
        }

        private void A_Click(object sender, EventArgs e)
        {
            Guna2Button a = sender as Guna2Button;
            Test test = context.Tests.FirstOrDefault(p => p.Test_Name == a.Text);
            List<Question> questions = context.Questions.Where(p => p.Test_Code == test.Test_Code).ToList();
            if (questions.Count != 0)
            {
                if (!frmHome.Instance.PnlContainer.Controls.ContainsKey("Testz"))
                {
                    Testz uc2 = new Testz(a.Text, owner);
                    uc2.Dock = DockStyle.Fill;
                    frmHome.Instance.PnlContainer.Controls.Add(uc2);
                }
                frmHome.Instance.PnlContainer.Controls["Testz"].BringToFront();
            }
            else
            {
                MessageBox.Show("Bài kiểm tra này chưa có câu hỏi nào!");
                return;
            }
        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Image.FromStream(memoryStream);
            }
        }
        private void HienThiLenComboBox(List<Test> listtest)
        {
            comboBox1.Items.Clear();
            foreach(var item in listtest)
            {
                comboBox1.Items.Add(item.Test_Name);
            }
        }
        private void HienThiLenListView()
        {
            listView1.Items.Clear();
            Test test = context.Tests.FirstOrDefault(p => p.Test_Name == comboBox1.Text);
            List<Test_Detail> test_Detail = context.Test_Detail.Where(p => p.Test_Code == test.Test_Code).ToList();
            List<Test_Detail> details = test_Detail.OrderByDescending(p => p.Test_Score).ToList();
            int index = 0;
            ImageList smallImage = new ImageList() { ImageSize = new Size(48, 48) };
            foreach (var item in details)
            {
                Ownere ownere = context.Owneres.FirstOrDefault(p => p.Ownere_Code == item.Ownere_Code);
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add((index + 1).ToString());
                if(ownere.Ownere_Image!=null)
                    smallImage.Images.Add(ConvertBinaryToImage(ownere.Ownere_Image));
                listView1.SmallImageList = smallImage;
                lvi.ImageIndex = index;
                lvi.SubItems.Add(ownere.Ownere_FullName);
                lvi.SubItems.Add(comboBox1.Text);
                lvi.SubItems.Add(item.Test_Score.ToString());
                listView1.Items.Add(lvi);
                index++;
            }
        }
        private void ChooseTestz_Load(object sender, EventArgs e)
        {
            List<Test> listtest = context.Tests.ToList();
            comboBox1.Text = "Level 1";
            HienThiLenComboBox(listtest);
            HienThiLenListView();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                HienThiLenListView();
        }
    }
}
