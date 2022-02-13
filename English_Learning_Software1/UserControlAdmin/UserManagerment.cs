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

namespace English_Learning_Software1.UserControlAdmin
{
    public partial class UserManagerment : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        public UserManagerment()
        {
            InitializeComponent();
            List<Ownere> owneres = context.Owneres.ToList();
            HienThiLenListView(owneres);

        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
                return Image.FromStream(memoryStream);
        }
        private void HienThiLenListView(List<Ownere> owneres)
        {
            ImageList smallImage = new ImageList() { ImageSize = new Size(48, 48) };
            listView1.Items.Clear();
            int index = 0;
            foreach (var item in owneres)
            {
                ListViewItem listviewitem = new ListViewItem();
                listviewitem.SubItems.Add((index + 1).ToString());
                listviewitem.SubItems.Add(item.Ownere_FullName);
                listviewitem.SubItems.Add(item.Ownere_Email);
                if(item.Ownere_PhoneNumber!=null)
                    listviewitem.SubItems.Add(item.Ownere_PhoneNumber);
                else
                    listviewitem.SubItems.Add("");
                listviewitem.SubItems.Add(item.Ownere_Birthday.Value.ToString());
                if (item.Ownere_Image != null)
                {
                    smallImage.Images.Add(ConvertBinaryToImage(item.Ownere_Image));
                }
                else
                {

                }
                listView1.SmallImageList = smallImage;
                listviewitem.ImageIndex = index;
                listView1.Items.Add(listviewitem);
                label2.Text = listView1.Items.Count.ToString();
                index++;
                
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == string.Empty) HienThiLenListView(context.Owneres.ToList());
            else
            {
                List<Ownere> supplies = context.Owneres.Where(p => p.Ownere_FullName.Trim().ToLower().Contains(guna2TextBox1.Text.Trim().ToLower())
                || p.Ownere_Email.Trim().ToLower().Contains(guna2TextBox1.Text.Trim().ToLower())).ToList();
                HienThiLenListView(supplies);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmAdmin.Instance.PnlContainer.Controls.Clear();
            Report uc2 = new Report();
            uc2.Dock = DockStyle.Fill;
            frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
        }
    }
}
