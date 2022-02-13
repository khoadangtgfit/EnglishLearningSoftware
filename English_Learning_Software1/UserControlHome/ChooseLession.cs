using English_Learning_Software1.Models;
using Guna.UI2.WinForms;
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
    public partial class ChooseLession : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        private string owner;
        public ChooseLession(string username)
        {
            InitializeComponent();
            owner = username;
        }
        private void LoadTypeWord()
        {
            List < Vocabulary_Type > listvocatype = context.Vocabulary_Type.ToList();
            foreach(Vocabulary_Type item in listvocatype)
            {
                Guna2Button a = new Guna2Button();
                a.FillColor = Color.FromArgb(91, 210, 240);
                a.Font= new Font(FontFamily.GenericSansSerif, 10.0F);
                a.BorderRadius = 8;
                a.Height = 45;
                a.Width = 180;
                a.Text = item.Vocabulary_Type_EN_Name;
                a.Dock = DockStyle.Top;
                a.Click += A_Click;
                flowLayoutPanel1.Controls.Add(a);
            }
        }
        private void A_Click(object sender, EventArgs e)
        {
            Guna2Button a = sender as Guna2Button;
            Vocabulary_Type vocatype = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == a.Text);
            List<Vocabulary> listvoca = context.Vocabularies.Where(p => p.Vocabulary_Type_Code == vocatype.Vocabulary_Type_Code).ToList();
            if (listvoca.Count!=0)
            {
                if (!frmHome.Instance.PnlContainer.Controls.ContainsKey("CheckUpVoca"))
                {
                    CheckUpVoca uc2 = new CheckUpVoca(a.Text, owner);
                    uc2.Dock = DockStyle.Fill;
                    frmHome.Instance.PnlContainer.Controls.Add(uc2);
                }
                frmHome.Instance.PnlContainer.Controls["CheckUpVoca"].BringToFront();
            }
            else
            {
                MessageBox.Show("Loại từ vựng này chưa có từ nào!");
                return;
            }
        }
        private void ChooseLession_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            LoadTypeWord();
            List<Vocabulary_Type> listtype = context.Vocabulary_Type.ToList();
            HienThiLenCombobox(listtype);
            guna2ComboBox1.SelectedIndex = 0;
        }
        private void HienThiLenCombobox(List<Vocabulary_Type> listtype)
        {
            guna2ComboBox1.Items.Clear();
            foreach (var item in listtype)
            {
                guna2ComboBox1.Items.Add(item.Vocabulary_Type_EN_Name);
            }
        }
        private void HienThiLenListView(List<Quiz_Detail> listquiz)
        {
            listView1.Items.Clear();
            int index = 0;
            List<Quiz_Detail> quiz_s = listquiz.OrderByDescending(p => p.Quiz_Score).ToList();
            foreach (Quiz_Detail item in quiz_s)
            {
                ListViewItem listviewitem = new ListViewItem((index + 1).ToString());
                Vocabulary_Type vocabulary_Type = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_Code == item.Vocabulary_Type_Code);
                listviewitem.SubItems.Add(vocabulary_Type.Vocabulary_Type_EN_Name);
                Account account = context.Accounts.FirstOrDefault(p => p.Ownere_Code == item.Ownere_Code);
                listviewitem.SubItems.Add(account.Account_Name);
                listviewitem.SubItems.Add(item.Quiz_Score.ToString());
                listView1.Items.Add(listviewitem);
                index++;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (guna2ComboBox1.SelectedIndex != -1)
                {
                    Vocabulary_Type a = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == guna2ComboBox1.Text);
                    if (a != null)
                    {
                        List<Quiz_Detail> quizzes = context.Quiz_Detail.Where(p => p.Vocabulary_Type_Code == a.Vocabulary_Type_Code).ToList();
                        HienThiLenListView(quizzes);
                    }
                }
                else
                {
                    MessageBox.Show("Mời bạn chọn loại từ vựng", "Thông báo");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
