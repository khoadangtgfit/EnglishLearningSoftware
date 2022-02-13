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

namespace English_Learning_Software1.UserControlAdmin
{
    public partial class StoriesManagerment : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        string fileName;
        public StoriesManagerment()
        {
            InitializeComponent();
            guna2Button2.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button3.Enabled = false;
            List<Story> stories = context.Stories.ToList();
            HienThiLenListView(stories);
        }
        Image image;
        Boolean flag = false;
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg", ValidateNames = true, Multiselect = false };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    image = Image.FromFile(fileName);
                    flag = true;
                    guna2CirclePictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
                return Image.FromStream(memoryStream);
        }
        private void stories_textchange(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == "" || gunaTextBox2.Text == "" || gunaTextBox3.Text == ""|| gunaTextBox4.Text == "")
            {
                guna2Button2.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button3.Enabled = false;
            }
            else
            {
                guna2Button2.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button3.Enabled = true;
            }
        }
        Story storythis;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Story story = context.Stories.FirstOrDefault(p => string.Compare(p.Story_Name, gunaTextBox3.Text, true) == 0);
                if (story != null)
                {
                    MessageBox.Show("Tên câu chuyện này đã tồn tại!");
                }
                else
                {
                    Story a = new Story();
                    a.Story_Name = gunaTextBox3.Text;
                    a.Story_EN_Content = gunaTextBox1.Text;
                    a.Story_VN_Content = gunaTextBox2.Text;
                    a.Story_Audio = gunaTextBox4.Text;
                    if (flag == false && storythis == null)
                        a.Story_Image = ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    if (flag == true)
                        a.Story_Image = ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    if (storythis != null && flag == false)
                        a.Story_Image = storythis.Story_Image;
                    context.Stories.AddOrUpdate(a);
                    context.SaveChanges();
                    List<Story> stories = context.Stories.ToList();
                    HienThiLenListView(stories);
                    MessageBox.Show("Thêm Thành Công");
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HienThiLenListView(List<Story> stories)
        {
            ImageList smallImage = new ImageList() { ImageSize = new Size(48, 48) };
            listView1.Items.Clear();
            int index = 0;
            foreach (var item in stories)
            {
                ListViewItem listviewitem = new ListViewItem();
                listviewitem.SubItems.Add((index + 1).ToString());
                listviewitem.SubItems.Add(item.Story_Name);
                if (item.Story_Image != null)
                {
                    smallImage.Images.Add(ConvertBinaryToImage(item.Story_Image));
                }
                else
                {

                }
                listView1.SmallImageList = smallImage;
                listviewitem.ImageIndex = index;
                listView1.Items.Add(listviewitem);
                index++;
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfile = new OpenFileDialog();
            opfile.Filter = "Video (.wmv)|*.wmv|Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
            if (opfile.ShowDialog() == DialogResult.OK)
            {
                gunaTextBox4.Text= opfile.SafeFileName;
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox3.Text != null)
                {
                    Story a = context.Stories.FirstOrDefault(p => string.Compare(p.Story_Name, gunaTextBox3.Text, true) == 0);
                    if (a == null)
                    {
                        MessageBox.Show("Không tìm thấy tên câu chuyện này!");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            context.Stories.Remove(a);
                            context.SaveChanges();
                            List<Story> liststory = context.Stories.ToList();
                            HienThiLenListView(liststory);
                            MessageBox.Show("Xóa thành công!");
                            LamMoi();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn câu chuyện cần xóa!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = listView1.SelectedItems[0];
                    string tb = lvi.SubItems[2].Text;
                    Story story = context.Stories.FirstOrDefault(p => p.Story_Name.ToString() == tb);
                    if (story != null)
                    {
                        storythis = story;
                        gunaTextBox3.Text = story.Story_Name;
                        gunaTextBox1.Text = story.Story_EN_Content;
                        gunaTextBox2.Text = story.Story_VN_Content;
                        gunaTextBox4.Text = story.Story_Audio;
                        guna2CirclePictureBox1.Image = ConvertBinaryToImage(story.Story_Image);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (storythis == null)
                {
                    MessageBox.Show("Vui lòng chọn câu chuyện cần sửa!");
                }
                else
                {
                    storythis.Story_Name = gunaTextBox3.Text;
                    storythis.Story_EN_Content = gunaTextBox1.Text;
                    storythis.Story_VN_Content = gunaTextBox2.Text;
                    storythis.Story_Audio = gunaTextBox4.Text;
                    if (flag == true)
                        storythis.Story_Image = ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    else
                        storythis.Story_Image = storythis.Story_Image;
                    context.Stories.AddOrUpdate(storythis);
                    context.SaveChanges();
                    List<Story> liststory = context.Stories.ToList();
                    HienThiLenListView(liststory);
                    MessageBox.Show("Sửa thành công!");
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LamMoi()
        {
            gunaTextBox1.Text = "";
            gunaTextBox1.Focus();
            gunaTextBox2.Text = "";
            gunaTextBox3.Text = "";
            gunaTextBox4.Text = "";
            gunaTextBox5.Text = "";
            guna2CirclePictureBox1.Image = Properties.Resources._1478594;
            flag = false;
            storythis = null;
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void gunaTextBox5_TextChanged(object sender, EventArgs e)
        {
          
            if (gunaTextBox5.Text == string.Empty) HienThiLenListView(context.Stories.ToList());
            else
            {
                List<Story> stories = context.Stories.Where(p => p.Story_Name.Trim().ToLower().Contains(gunaTextBox5.Text.Trim().ToLower())).ToList();
                HienThiLenListView(stories);
            }
        }
    }
    
}
