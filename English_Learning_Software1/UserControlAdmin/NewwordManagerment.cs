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
    public partial class NewwordManagerment : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        string fileName;
        public NewwordManagerment()
        {
            InitializeComponent();
            guna2Button2.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button3.Enabled = false;
            List<Vocabulary> listvoca = context.Vocabularies.ToList();
            HienThiLenListView(listvoca);
            List<Vocabulary_Type> listvocatype = context.Vocabulary_Type.ToList();
            HienThiLoaiTuLenCombobox(listvocatype);
            gunaComboBox1.SelectedIndex = 0;
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
        private void HienThiLoaiTuLenCombobox(List<Vocabulary_Type> listvocatype)
        {
            this.gunaComboBox1.DataSource = listvocatype;
            this.gunaComboBox1.DisplayMember = "Vocabulary_Type_EN_Name";
            this.gunaComboBox1.ValueMember = "Vocabulary_Type_Code";
        }
        private void Newword_TextChange(object sender,EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && gunaComboBox1.Text != "")
            {
                guna2Button2.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button3.Enabled = true;
            }
            else
            {
                guna2Button2.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button3.Enabled = false;
            }
        }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg", ValidateNames = true, Multiselect = false };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    image= Image.FromFile(fileName);
                    flag = true;
                    guna2CirclePictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HienThiLenListView(List<Vocabulary> listvoca)
        {
            ImageList smallImage = new ImageList() { ImageSize = new Size(48, 48) };
            listView1.Items.Clear();
            int index = 0;
            foreach(var item in listvoca)
            {
                ListViewItem listviewitem = new ListViewItem();
                listviewitem.SubItems.Add((index+1).ToString());
                listviewitem.SubItems.Add(item.English_Vocabulary);
                listviewitem.SubItems.Add(item.Vietnamese_Vocabulary);
                listviewitem.SubItems.Add(item.Vocabulary_Type.Vocabulary_Type_EN_Name);
                if (item.Vocabulary_Image != null)
                {
                    smallImage.Images.Add(ConvertBinaryToImage(item.Vocabulary_Image));
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
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Vocabulary voca = context.Vocabularies.FirstOrDefault(p => string.Compare(p.English_Vocabulary,guna2TextBox1.Text,true)==0);
                if (voca != null)
                {
                    MessageBox.Show("Từ này đã tồn tại!","Thông báo");
                }
                else
                {
                    Vocabulary a = new Vocabulary();
                    a.English_Vocabulary = guna2TextBox1.Text;
                    a.Vietnamese_Vocabulary = guna2TextBox2.Text;
                    Vocabulary_Type b = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == gunaComboBox1.Text);
                    a.Vocabulary_Type_Code = b.Vocabulary_Type_Code;
                    if(flag==false&&vocathis==null)
                        a.Vocabulary_Image= ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    if (flag == true )
                        a.Vocabulary_Image = ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    if(vocathis!=null&&flag==false)
                        a.Vocabulary_Image = vocathis.Vocabulary_Image;
                    context.Vocabularies.AddOrUpdate(a);
                    context.SaveChanges();
                    List<Vocabulary> listvoca = context.Vocabularies.ToList();
                    HienThiLenListView(listvoca);
                    LamMoi();
                    MessageBox.Show("Thêm Thành Công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Image image;
        Vocabulary vocathis;
        Boolean flag = false;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = listView1.SelectedItems[0];
                    guna2TextBox1.Text = lvi.SubItems[2].Text;
                    guna2TextBox2.Text = lvi.SubItems[3].Text;
                    string str = lvi.SubItems[4].Text;
                    Vocabulary_Type a = context.Vocabulary_Type.ToList().FirstOrDefault(p => p.Vocabulary_Type_EN_Name.ToString() == str);
                    if (a != null)
                    {
                        gunaComboBox1.Text = a.Vocabulary_Type_EN_Name;
                    }
                    Vocabulary c = context.Vocabularies.ToList().FirstOrDefault(p => p.English_Vocabulary == lvi.SubItems[2].Text);
                    if (c != null)
                    {
                        image = ConvertBinaryToImage(c.Vocabulary_Image);
                        guna2CirclePictureBox1.Image = image;
                        vocathis = c;
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
                if (vocathis == null)
                {
                    MessageBox.Show("Vui lòng chọn từ cần sửa!");
                }
                else
                {
                    vocathis.English_Vocabulary = guna2TextBox1.Text;
                    vocathis.Vietnamese_Vocabulary = guna2TextBox2.Text;
                    if (gunaComboBox1.SelectedIndex != -1)
                    {
                        Vocabulary_Type voca1 = context.Vocabulary_Type.FirstOrDefault(p => p.Vocabulary_Type_EN_Name == gunaComboBox1.Text);
                        if (voca1 != null)
                        {
                            vocathis.Vocabulary_Type_Code = voca1.Vocabulary_Type_Code;
                        }
                    }
                    if (flag == true)
                        vocathis.Vocabulary_Image = ConvertImageToBinary(guna2CirclePictureBox1.Image);
                    else
                        vocathis.Vocabulary_Image = vocathis.Vocabulary_Image;
                    context.Vocabularies.AddOrUpdate(vocathis);
                    context.SaveChanges();
                    List<Vocabulary> listvoca = context.Vocabularies.ToList();
                    HienThiLenListView(listvoca);
                    MessageBox.Show("Sửa thành công!");
                    LamMoi();
                }
               
        }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void LamMoi()
        {
            guna2CirclePictureBox1.Image = Properties.Resources._1478594;
            guna2TextBox1.Text = "";
            guna2TextBox1.Focus();
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            gunaComboBox1.SelectedIndex = 0;
            vocathis = null;
            flag = false;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != null)
                {
                    Vocabulary a = context.Vocabularies.FirstOrDefault(p => string.Compare(p.English_Vocabulary, guna2TextBox1.Text, true) == 0);
                    if (a == null)
                    {
                        MessageBox.Show("Không tìm thấy từ này!");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa?", "Câu hỏi", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            context.Vocabularies.Remove(a);
                            context.SaveChanges();
                            List<Vocabulary> listvoca = context.Vocabularies.ToList();
                            HienThiLenListView(listvoca);
                            LamMoi();
                            MessageBox.Show("Xóa thành công!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập từ cần xóa!");
                }
             }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == string.Empty) HienThiLenListView(context.Vocabularies.ToList());
            else
            {
                List<Vocabulary> listvoca = context.Vocabularies.Where(p => p.English_Vocabulary.Trim().ToLower().Contains(guna2TextBox3.Text.Trim().ToLower())
                || p.Vietnamese_Vocabulary.Trim().ToLower().Contains(guna2TextBox3.Text.Trim().ToLower())
                || p.Vocabulary_Type.Vocabulary_Type_EN_Name.Trim().ToLower().Contains(guna2TextBox3.Text.Trim().ToLower())).ToList();
                HienThiLenListView(listvoca);
            }
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox4.Text == "")
                {
                    MessageBox.Show("Yêu cầu nhập!");
                }
                else
                {
                    Vocabulary_Type a = context.Vocabulary_Type.FirstOrDefault(p => string.Compare(p.Vocabulary_Type_EN_Name, guna2TextBox4.Text, true) == 0);
                    if (a == null)
                    {
                        Vocabulary_Type b = new Vocabulary_Type();
                        b.Vocabulary_Type_EN_Name = guna2TextBox4.Text;
                        context.Vocabulary_Type.AddOrUpdate(b);
                        context.SaveChanges();
                        List<Vocabulary_Type> listtype = context.Vocabulary_Type.ToList();
                        HienThiLoaiTuLenCombobox(listtype);
                        MessageBox.Show("Thêm thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Tên loại đã tồn tại!");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            LamMoi();
        }
    }
    
}
