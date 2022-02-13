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
    public partial class Videoz : UserControl
    {
        public Videoz()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.Ctlenabled = true;
        }
        EnglishDBContext context = new EnglishDBContext();
        private void HienThiLenListBox()
        {
            listBox1.Items.Clear();
            List<Video> listvideo = context.Videos.ToList();
            foreach(Video item in listvideo)
            {
                if (item.Video_Name != "")
                {
                    listBox1.Items.Add(item.Video_Name);
                }
            }
        }
        private void Videoz_Load(object sender, EventArgs e)
        {
            HienThiLenListBox();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Video a = context.Videos.FirstOrDefault(p => p.Video_Name == listBox1.SelectedItem.ToString());
                if(a!=null)
                    axWindowsMediaPlayer1.URL = a.Video_Path;
            }
        }

        private void axWindowsMediaPlayer1_Leave(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
    }
}
