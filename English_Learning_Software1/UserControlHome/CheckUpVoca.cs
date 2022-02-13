using English_Learning_Software1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class CheckUpVoca : UserControl
    {
        private string typeword;
        private SoundPlayer correct;
        private SoundPlayer wrong;
        EnglishDBContext context =new EnglishDBContext();
        private string owner;
        public CheckUpVoca(string type,string username)
        {
            InitializeComponent();
            correct = new SoundPlayer("correct.wav");
            wrong = new SoundPlayer("wrongone.wav");
            this.typeword = type;
            owner = username;
            frmHome.Instance.PnlContainer1.Enabled = false;
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        List<Vocabulary> listvoca;
        private void CreateList(string tb)
        {
            Vocabulary_Type a = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == tb);
            if (a != null)
                listvoca = context.Vocabularies.ToList().Where(p => p.Vocabulary_Type_Code == a.Vocabulary_Type_Code).ToList();
        }
        int dung;
        int index;
        private Vocabulary word;
        private void CheckUpVoca_Load(object sender, EventArgs e)
        {
            CreateList(typeword);
            dung = 0;
            index = 0;
            Vocabulary a = listvoca[index];
            word = a;
            pictureBox1.Image = byteArrayToImage(a.Vocabulary_Image);
            label1.Text = "Câu thứ: " + (index + 1) + "/" + listvoca.Count;
            label2.Text = "Số câu đúng:" + dung + "/" + listvoca.Count;
            label3.Text = "Số điểm: 0";
            guna2Button2.Enabled = false;
        }
        private void reset()
        {
            guna2TextBox1.Text = "";
            guna2TextBox1.ReadOnly = false;
            Color clo = Color.White;
            panel1.BackColor = clo;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (listvoca.Count - 1 <= index)
            {
                float a = (dung * 100) / listvoca.Count;
                Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == owner);
                Ownere ownere = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
                Vocabulary_Type vocatype = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == typeword);
                Quiz_Detail quiz_Detail = context.Quiz_Detail.FirstOrDefault(p => p.Ownere_Code == ownere.Ownere_Code && p.Vocabulary_Type_Code == vocatype.Vocabulary_Type_Code);
                if (quiz_Detail != null && a >= quiz_Detail.Quiz_Score)
                {
                    quiz_Detail.Quiz_Score = a;
                    context.Quiz_Detail.AddOrUpdate(quiz_Detail);
                    context.SaveChanges();
                }
                if (quiz_Detail == null)
                {
                    Quiz_Detail quiz = new Quiz_Detail();
                    quiz.Ownere_Code = ownere.Ownere_Code;
                    quiz.Vocabulary_Type_Code = vocatype.Vocabulary_Type_Code;
                    quiz.Quiz_Score = a;
                    context.Quiz_Detail.AddOrUpdate(quiz);
                    context.SaveChanges();
                }
                frmHome.Instance.PnlContainer.Controls.Clear();
                ChooseLession uc2 = new ChooseLession(owner);
                uc2.Dock = DockStyle.Fill;
                frmHome.Instance.PnlContainer.Controls.Add(uc2);
                frmHome.Instance.PnlContainer1.Enabled = true;
            }
            else
            {
                Vocabulary a = listvoca[++index];
                word = a;
                pictureBox1.Image = byteArrayToImage(a.Vocabulary_Image);
                label1.Text = "Câu thứ: "+(index + 1) + "/" + listvoca.Count;
                guna2Button1.Enabled = true;
                guna2Button2.Enabled = false;
            }
            reset();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Bạn cần nhập");
                return;
            }
            if (string.Compare(guna2TextBox1.Text, word.English_Vocabulary, true) == 0)
            {
                Color clo = Color.FromArgb(131, 255, 36);
                panel1.BackColor = clo;
                dung++;
                label2.Text = "Số câu đúng:" + dung + "/" + listvoca.Count;
                float a = (dung * 100) / listvoca.Count;
                label3.Text = "Số điểm: "+a + "";
                correct.Play();
            }
            else
            {
                Color clo = Color.FromArgb(255, 47, 80);
                panel1.BackColor = clo;
                wrong.Play();
            }
            guna2TextBox1.ReadOnly = true;
            guna2Button1.Enabled = false;
            guna2Button2.Enabled = true;
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có thực sự muốn thoát?", "Câu hỏi", MessageBoxButtons.YesNo);
            if (a == DialogResult.Yes)
            {
                frmHome.Instance.PnlContainer.Controls.Clear();
                ChooseLession uc2 = new ChooseLession(owner);
                uc2.Dock = DockStyle.Fill;
                frmHome.Instance.PnlContainer.Controls.Add(uc2);
                frmHome.Instance.PnlContainer1.Enabled = true;
            }
        }
    }
}
