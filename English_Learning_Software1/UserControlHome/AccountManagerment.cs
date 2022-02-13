using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using English_Learning_Software1.Models;

namespace English_Learning_Software1.UserControlHome
{
    public partial class AccountManagerment : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        string fileName;
        Ownere ownerthis;
        public AccountManagerment(string username)
        {
            InitializeComponent();
            Account account = context.Accounts.Where(p => p.Account_Name == username).SingleOrDefault();
            Ownere ownere = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
            ownerthis = ownere;
            guna2TextBox1.Text = account.Ownere.Ownere_FullName;
            guna2TextBox3.Text = account.Ownere.Ownere_Email;
            guna2TextBox1.MaxLength = 40;
            guna2TextBox2.MaxLength = 10;
            guna2TextBox3.MaxLength = 30;
            if (string.IsNullOrEmpty(account.Ownere.Ownere_PhoneNumber) == false)
            {
                gunaCirclePictureBox1.Image = ConvertBinaryToImage(account.Ownere.Ownere_Image);
                guna2TextBox2.Text = account.Ownere.Ownere_PhoneNumber;
            }
            else
            {
                guna2TextBox2.Text = "";
            }
            if (account.Ownere.Ownere_Birthday.ToString() != "")
            {
                dateTimePicker3.Value = account.Ownere.Ownere_Birthday.Value;
            }
            else
            {
                dateTimePicker3.Value = DateTime.Now;
            }
        }
        private byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                return memoryStream.ToArray();
            }
        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Image.FromStream(memoryStream);
            }
        }
        bool flag = false;
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg", ValidateNames = true, Multiselect = false };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    flag = true;
                    gunaCirclePictureBox1.Image = Image.FromFile(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool IsValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }
        public bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            string sMailPattern = @"^((093(\d){7})|(035(\d){7})|(086(\d){7})|(076(\d){7}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "")
                {
                    if (IsValid(guna2TextBox3.Text))
                    {
                        if (IsValidVietNamPhoneNumber(guna2TextBox2.Text))
                        {
                            ownerthis.Ownere_FullName = guna2TextBox1.Text;
                            ownerthis.Ownere_Email = guna2TextBox3.Text;
                            ownerthis.Ownere_PhoneNumber = guna2TextBox2.Text;
                            ownerthis.Ownere_Birthday = dateTimePicker3.Value;
                            if (flag == true)
                                ownerthis.Ownere_Image = ConvertImageToBinary(gunaCirclePictureBox1.Image);
                            context.Owneres.AddOrUpdate(ownerthis);
                            context.SaveChanges();
                            MessageBox.Show("Thay đổi thành công", " Thông báo");
                            guna2Button1.Enabled = false;
                            guna2Button2.Enabled = true;
                            guna2TextBox1.Enabled = false;
                            guna2TextBox2.Enabled = false;
                            guna2TextBox3.Enabled = false;
                            dateTimePicker3.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Số điện thoại không hợp lệ!","Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email không hợp lệ!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", " Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AccountManagerment_Load(object sender, EventArgs e)
        {
            guna2Button1.Enabled = false;
            guna2Button2.Enabled = true;
            guna2TextBox1.Enabled = false;
            guna2TextBox2.Enabled = false;
            guna2TextBox3.Enabled = false;
            dateTimePicker3.Enabled = false;
            gunaCirclePictureBox1.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button1.Enabled = true;
            guna2Button2.Enabled = false;
            guna2TextBox1.Enabled = true;
            guna2TextBox2.Enabled = true;
            guna2TextBox3.Enabled = true;
            dateTimePicker3.Enabled = true;
            gunaCirclePictureBox1.Enabled = true;
        }
    }
}
