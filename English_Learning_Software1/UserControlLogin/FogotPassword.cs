using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using English_Learning_Software1.Models;
namespace English_Learning_Software1.UserControlLogin
{
    public partial class FogotPassword : UserControl
    {
        public FogotPassword()
        {
            InitializeComponent();
            guna2TextBox1.MaxLength = 30;
            //guna2TextBox2.MaxLength = 20;
            //guna2TextBox3.MaxLength = 20;
            //guna2TextBox4.MaxLength = 6;
            guna2Button3.Enabled = false;
        }
        EnglishDBContext context = new EnglishDBContext();
        private void Textchange_forgotpassword(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && guna2TextBox4.Text != "")
            {
                guna2Button3.Enabled = true;
            }
            else
            {
                guna2Button3.Enabled = false;
            }
        }
        private void resetz()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
        }
        public bool IsValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (context.Accounts.Where(p => p.Account_Name == guna2TextBox2.Text && p.Ownere.Ownere_Email == guna2TextBox1.Text).Any() == true)
                {
                    if (guna2TextBox4.Text == reCode.ToString())
                    {
                        if (guna2TextBox3.Text.Length < 6 || guna2TextBox3.Text.Length > 16)
                        {
                            MessageBox.Show("Mật khẩu phải từ 6 đến 16 ký tự!");
                        }
                        else
                        {
                            Account account = context.Accounts.Where(p => p.Account_Name == guna2TextBox2.Text && p.Ownere.Ownere_Email == guna2TextBox1.Text).SingleOrDefault();
                            account.Account_Password = guna2TextBox3.Text;
                            context.Accounts.AddOrUpdate(account);
                            context.SaveChanges();
                            MessageBox.Show("Thay đổi mật khẩu thành công", "Thông báo");
                            resetz();
                        }   
                    }
                    else
                    {
                        MessageBox.Show("Mã xác nhận không đúng", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc email không đúng", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Mã thay đổi mật khẩu đã được gửi vào hộp thư của bạn", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2TextBox3_IconRightClick(object sender, EventArgs e)
        {
            if (guna2TextBox3.UseSystemPasswordChar == true)
            {
                guna2TextBox3.IconRight = Properties.Resources.hidden;
                guna2TextBox3.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox3.IconRight = Properties.Resources.view1;
                guna2TextBox3.UseSystemPasswordChar = true;
            }
        }
        int reCode;
        Random rd = new Random();

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid(guna2TextBox1.Text))
                {
                    SmtpClient Client = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        //UseDefaultCredentials = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential()
                        {
                            UserName = "garena281215@gmail.com",
                            Password = "lcxehypdkkvzyzqh",
                        }
                    };
                    MailAddress FromGMail = new MailAddress("garena281215@gmail.com", "HKT Company");
                    MailAddress ToGMail = new MailAddress(guna2TextBox1.Text, "Me");
                    reCode = rd.Next(100000, 999999);
                    MailMessage Message = new MailMessage()
                    {
                        From = FromGMail,
                        Subject = "Thay đổi mật khẩu",
                        Body = "Mã thay đổi mật khẩu của bạn là: " + reCode.ToString(),
                        Priority = MailPriority.High,
                    };
                    Message.To.Add(ToGMail);
                    Client.SendCompleted += Client_SendCompleted;
                    Client.SendMailAsync(Message);
                }
                else
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!frmLogin.Instance.PnlContainer.Controls.ContainsKey("Login"))
            {
                Login uc2 = new Login();
                uc2.Dock = DockStyle.Fill;
                frmLogin.Instance.PnlContainer.Controls.Add(uc2);
                guna2TextBox1.Focus();
            }
            frmLogin.Instance.PnlContainer.Controls["Login"].BringToFront();

        }
    }
}
