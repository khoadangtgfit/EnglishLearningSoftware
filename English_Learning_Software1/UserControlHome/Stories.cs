using English_Learning_Software1.Models;
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
    public partial class Stories : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        string namestory;
        string ownerz;
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
                return Image.FromStream(memoryStream);
        }
        public Stories(string storyname,string owner)
        {
            InitializeComponent();
            namestory = storyname;
            ownerz = owner;
            HienThi();
        }
        private void HienThi()
        {
            Story a = context.Stories.FirstOrDefault(p => p.Story_Name == namestory);
            if (a != null)
            {
                label3.Text = a.Story_Name;
                label1.Text = a.Story_EN_Content;
                label2.Text = a.Story_VN_Content;
                pictureBox1.Image = ConvertBinaryToImage(a.Story_Image);
            }
        }
        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            Story a = context.Stories.FirstOrDefault(p => p.Story_Name == namestory);
            if (a != null)
            {
                axWindowsMediaPlayer1.URL = a.Story_Audio;
            }
        }

        private void Stories_Leave(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.close();
            frmHome.Instance.PnlContainer.Controls.Clear();
            ChooseStories uc2 = new ChooseStories(ownerz);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
            frmHome.Instance.PnlContainer.Controls["ChooseStories"].BringToFront();
        }
    }
}
