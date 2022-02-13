using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English_Learning_Software1.Models;

namespace English_Learning_Software1.UserControlLogin
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            guna2Button1.Enabled = false;
            guna2TextBox1.Focus();
            guna2TextBox2.MaxLength = 20;
            if (Properties.Settings.Default.Rememberme == true)
            {
                if (Properties.Settings.Default.Username != null && Properties.Settings.Default.Password != null)
                {
                    guna2TextBox1.Text = Properties.Settings.Default.Username;
                    guna2TextBox2.Text = Properties.Settings.Default.Password;
                    guna2ToggleSwitch1.Checked = true;
                    Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == Properties.Settings.Default.Username);
                    if (account.Account_Type_Code == false)
                    {
                        frmLogin.Instance.PnlContainer1.Checked = false;
                        frmHome frmHome = new frmHome(Properties.Settings.Default.Username);
                        frmHome.ShowDialog();
                    }
                    else
                    {
                        frmLogin.Instance.PnlContainer1.Checked = false;
                        frmAdmin frmAdmin = new frmAdmin(Properties.Settings.Default.Username);
                        frmAdmin.ShowDialog();
                    }
                }

            }
            else
            {
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
            }
            
        }
        EnglishDBContext context = new EnglishDBContext();
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "")
            {
                guna2Button1.Enabled = true;
            }
            else
            {
                guna2Button1.Enabled = false;
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!frmLogin.Instance.PnlContainer.Controls.ContainsKey("SignUp"))
            {
                SignUp uc2 = new SignUp();
                uc2.Dock = DockStyle.Fill;
                frmLogin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmLogin.Instance.PnlContainer.Controls["SignUp"].BringToFront();
        }
        private void resetz()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
        }
        private void REMEBERME()
        {

            if (guna2ToggleSwitch1.Checked == true)
            {
                Properties.Settings.Default.Username = guna2TextBox1.Text;
                Properties.Settings.Default.Password = guna2TextBox2.Text;
                Properties.Settings.Default.Rememberme = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Rememberme = false;
                Properties.Settings.Default.Save();
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (context.Accounts.Where(p => p.Account_Name == guna2TextBox1.Text && p.Account_Password == guna2TextBox2.Text && p.Account_Type_Code == true).Any() == true)
                {
                    REMEBERME();
                    frmLogin.Instance.PnlContainer1.Checked = false;
                    frmAdmin frmAdmin = new frmAdmin(guna2TextBox1.Text);
                    frmAdmin.ShowDialog();
                    if (guna2ToggleSwitch1.Checked == false)
                    {
                        resetz();
                    }
                }
                else if (context.Accounts.Where(p => p.Account_Name == guna2TextBox1.Text && p.Account_Password == guna2TextBox2.Text && p.Account_Type_Code == false).Any() == true)
                {
                    REMEBERME();
                    frmLogin.Instance.PnlContainer1.Checked = false;
                    frmHome frmHome = new frmHome(guna2TextBox1.Text);
                    frmHome.ShowDialog();
                    if (guna2ToggleSwitch1.Checked == false)
                    {
                        resetz();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2TextBox2_IconRightClick(object sender, EventArgs e)
        {
            if (guna2TextBox2.UseSystemPasswordChar == true)
            {
                guna2TextBox2.IconRight = Properties.Resources.hidden;
                guna2TextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox2.IconRight = Properties.Resources.view1;
                guna2TextBox2.UseSystemPasswordChar = true;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            if (!frmLogin.Instance.PnlContainer.Controls.ContainsKey("FogotPassword"))
            {
                FogotPassword FG2 = new FogotPassword();
                FG2.Dock = DockStyle.Fill;
                frmLogin.Instance.PnlContainer.Controls.Add(FG2);
            }
            frmLogin.Instance.PnlContainer.Controls["FogotPassword"].BringToFront();
        }
    }
}
