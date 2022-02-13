using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
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
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
            guna2Button2.Enabled = false;
            guna2TextBox1.MaxLength = 40;
            guna2TextBox2.MaxLength = 30;
            //guna2TextBox3.MaxLength = 20;
            guna2TextBox4.MaxLength = 20;
            guna2TextBox1.Focus();

        }
        EnglishDBContext context = new EnglishDBContext();
        private void Textchange_signup(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && guna2TextBox4.Text != "")
            {
                guna2Button2.Enabled = true;
            }
            else
            {
                guna2Button2.Enabled = false;
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
        private void resetz()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid(guna2TextBox2.Text))
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
                    MailAddress ToGMail = new MailAddress(guna2TextBox2.Text, "Me");
                    MailMessage Message = new MailMessage()
                    {
                        From = FromGMail,
                        Subject = "Chào mừng thành viên mới",
                        Body = "Xin chào " + guna2TextBox1.Text + ",\nChào mừng bạn đến với phần mềm học tiếng anh Learning English HKT." +
                        "\n Cảm ơn bạn đã sử dụng và chúc bạn có trải nghiệm tốt!\n Bất cứ thắc mắc hoặc yêu cầu giúp đỡ xin vui lòng liên hệ qua email: tudeyer@gmail.com",
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
        private void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
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
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid(guna2TextBox2.Text))
                {
                    if ((guna2TextBox3.Text.Length < 6 || guna2TextBox3.Text.Length > 16) || (guna2TextBox4.Text.Length < 6 || guna2TextBox4.Text.Length > 16))
                    {
                        MessageBox.Show("Tên tài khoản và mật khẩu phải có 6 đến 16 ký tự", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (context.Accounts.Where(p => p.Account_Name.ToString().ToLower().Trim() == guna2TextBox3.Text.ToLower().Trim()).Any() == true)
                        {
                            MessageBox.Show("Tên tài khoản đã tồn tại", "Thông báo");
                        }
                        else
                        {

                            if (context.Owneres.Where(p => p.Ownere_FullName == guna2TextBox1.Text && p.Ownere_Email == guna2TextBox2.Text).Any() == false)
                            {
                                Ownere own = new Ownere();
                                own.Ownere_FullName = guna2TextBox1.Text;
                                own.Ownere_Email = guna2TextBox2.Text;
                                own.Ownere_Image = ConvertImageToBinary(Properties.Resources.user);
                                own.Ownere_Birthday = DateTime.Now;
                                context.Owneres.AddOrUpdate(own);
                                context.SaveChanges();
                            }
                            Account account = new Account();
                            account.Account_Name = guna2TextBox3.Text;
                            account.Account_Password = guna2TextBox4.Text;
                            account.Account_Type_Code = false;
                            Ownere own1 = context.Owneres.Where(p => p.Ownere_Email == guna2TextBox2.Text && p.Ownere_FullName == guna2TextBox1.Text).SingleOrDefault();
                            account.Ownere_Code = own1.Ownere_Code;
                            context.Accounts.AddOrUpdate(account);
                            context.SaveChanges();
                            MessageBox.Show("Tạo tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            guna2Button3_Click(sender,e);
                            resetz();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Email không hợp lệ!","Thông báo");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2TextBox4_IconRightClick(object sender, EventArgs e)
        {
            if (guna2TextBox4.UseSystemPasswordChar == true)
            {
                guna2TextBox4.IconRight = Properties.Resources.hidden;
                guna2TextBox4.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox4.IconRight = Properties.Resources.view;
                guna2TextBox4.UseSystemPasswordChar = true;
            }
        }

    }
}
