using English_Learning_Software1.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class NewwordImage : UserControl
    {
        public NewwordImage()
        {
            InitializeComponent();
        }
        EnglishDBContext context = new EnglishDBContext();
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void LoadTypeWord()
        {
            List<Vocabulary_Type> listvocatype = context.Vocabulary_Type.ToList();
            foreach (Vocabulary_Type item in listvocatype)
            {
                Guna2Button a = new Guna2Button();
                Color color = Color.FromArgb(31, 218, 154);
                a.FillColor = color;
                a.TextAlign = HorizontalAlignment.Center;
                a.Font= new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);
                a.Text = item.Vocabulary_Type_EN_Name;
                a.Dock = DockStyle.Top;
                a.Click += A_Click;
                flowLayoutPanel2.Controls.Add(a);
            }
        }
        String tb;
        private void A_Click(object sender, EventArgs e)
        {
            Guna2Button a = sender as Guna2Button;
            tb = a.Text;
            Vocabulary_Type type = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == tb);
            List<Vocabulary> vocabularies = context.Vocabularies.Where(p => p.Vocabulary_Type_Code == type.Vocabulary_Type_Code).ToList();
            if (vocabularies.Count == 0)
            {
                MessageBox.Show("Loại từ này chưa có từ vựng nào!","Thông báo");
            }
            else
            {
                GetData(tb);
            }
        }

        private void GetData(string tb)
        {
            flowLayoutPanel3.Controls.Clear();
            List<Vocabulary> listnw = context.Vocabularies.ToList();
            foreach (Vocabulary item in listnw)
            {
                Vocabulary_Type vocatype = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == tb);
                if (vocatype != null)
                {
                    if (item.Vocabulary_Type_Code == vocatype.Vocabulary_Type_Code)
                    {
                        PictureBox a = new PictureBox();
                        a.BackgroundImageLayout = ImageLayout.Zoom;
                        a.BorderStyle = BorderStyle.FixedSingle;
                        a.SizeMode = PictureBoxSizeMode.StretchImage;
                        a.Width = a.Height = 200;
                        a.Image = byteArrayToImage(item.Vocabulary_Image);
                        Label lb = new Label();
                        lb.Font= new Font(FontFamily.GenericSansSerif,9.0F);
                        lb.ForeColor = Color.White;
                        lb.Text = item.English_Vocabulary;
                        lb.TextAlign = ContentAlignment.MiddleCenter;
                        lb.BackColor = Color.FromArgb(37, 131, 227);
                        lb.Dock = DockStyle.Bottom;
                        Label lb1 = new Label();
                        lb1.Font= new Font(FontFamily.GenericSansSerif, 9.0F);
                        lb1.ForeColor = Color.White;
                        lb1.Text = item.Vietnamese_Vocabulary;
                        lb1.TextAlign = ContentAlignment.TopCenter;
                        lb1.BackColor = Color.FromArgb(250, 105, 0);
                        a.Name = item.English_Vocabulary;
                        a.Controls.Add(lb);
                        a.Controls.Add(lb1);
                        a.Click += A_Click1;
                        flowLayoutPanel3.Controls.Add(a);
                    }
                }
                
            }
        }
        SpeechSynthesizer sp = new SpeechSynthesizer();
        private void A_Click1(object sender, EventArgs e)
        {
            PictureBox a = sender as PictureBox;
            a.Tag = Color.Blue;
            if (a.Name != "")
            {
                sp.SelectVoice("Microsoft Zira Desktop");
                sp.SpeakAsync(a.Name);
            }
        }

        private void NewwordImage_Load(object sender, EventArgs e)
        {
            LoadTypeWord();
        }
    }
}
