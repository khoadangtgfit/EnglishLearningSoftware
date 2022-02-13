using English_Learning_Software1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class ChooseStories : UserControl
    {
        private string owner;
        public ChooseStories(string username)
        {
            InitializeComponent();
            owner = username;
        }
        EnglishDBContext context = new EnglishDBContext();
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void GetData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                List<Story> listnw = context.Stories.ToList();
                foreach (Story item in listnw)
                {
                    Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == owner);
                    Ownere ownere = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
                    Story story1 = context.Stories.FirstOrDefault(p => p.Story_Name == item.Story_Name);
                    Story_Detail story_Detail = context.Story_Detail.FirstOrDefault(p => p.Story_Code == story1.Story_Code && p.Ownere_Code == ownere.Ownere_Code);
                    Label label = new Label();
                    if (story_Detail == null)
                    {
                        label.Text = "Chưa xem";
                    }
                    else
                    {
                        label.Text = "Đã xem";
                    }
                    PictureBox a = new PictureBox();
                    a.BorderStyle = BorderStyle.FixedSingle;
                    a.BackgroundImageLayout = ImageLayout.Zoom;
                    a.SizeMode = PictureBoxSizeMode.StretchImage;
                    a.Width = a.Height = 200;
                    a.Image = byteArrayToImage(item.Story_Image);
                    Label lb = new Label();
                    lb.ForeColor = Color.White;
                    lb.Font = new Font(FontFamily.GenericSansSerif, 9.0F);
                    lb.Text = item.Story_Name;
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    lb.BackColor = Color.FromArgb(37, 131, 227);
                    lb.Dock = DockStyle.Bottom;
                    a.Name = item.Story_Name;
                    label.Font = new Font(FontFamily.GenericSansSerif, 9.0F);
                    label.TextAlign = ContentAlignment.TopCenter;
                    label.BackColor = Color.FromArgb(250, 105, 0);
                    label.ForeColor = Color.White;
                    a.Controls.Add(lb);
                    a.Controls.Add(label);
                    a.Click += A_Click1;
                    flowLayoutPanel1.Controls.Add(a);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void A_Click1(object sender, EventArgs e)
        {
            try
            {
                PictureBox a = sender as PictureBox;
                frmHome.Instance.PnlContainer.Controls.Clear();
                Stories uc2 = new Stories(a.Name, owner);
                uc2.Dock = DockStyle.Fill;
                frmHome.Instance.PnlContainer.Controls.Add(uc2);
                Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == owner);
                Ownere ownere = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
                Story story1 = context.Stories.FirstOrDefault(p => p.Story_Name == a.Name);
                Story_Detail story_Detail = context.Story_Detail.FirstOrDefault(p => p.Story_Code == story1.Story_Code && p.Ownere_Code == ownere.Ownere_Code);
                if (story_Detail == null)
                {
                    Story_Detail story_Detail1 = new Story_Detail();
                    story_Detail1.Ownere_Code = ownere.Ownere_Code;
                    story_Detail1.Story_Code = story1.Story_Code;
                    story_Detail1.Story_Detail_Status = "Đã xem";
                    context.Story_Detail.AddOrUpdate(story_Detail1);
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ChooseStories_Load(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
