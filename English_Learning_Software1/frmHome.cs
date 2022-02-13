using English_Learning_Software1.UserControlHome;
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
using System.IO;


namespace English_Learning_Software1
{
    public partial class frmHome : Form
    {
        public static string owner;
        EnglishDBContext context = new EnglishDBContext();
        public frmHome(string username)
        {
            InitializeComponent();
            panel7.Visible = false;
            panel8.Visible = false;
            owner = username;
            Account account = context.Accounts.Where(p => p.Account_Name == username).SingleOrDefault();
            label3.Text = account.Ownere.Ownere_FullName;
            if (string.IsNullOrEmpty(account.Ownere.Ownere_PhoneNumber) == false)
            {
                gunaCirclePictureBox1.Image = ConvertBinaryToImage(account.Ownere.Ownere_Image);
            }
            _obj = this;
            Home1 uc3 = new Home1();
            uc3.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc3);
        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Image.FromStream(memoryStream);
            }
        }
        static frmHome _obj;

        public static frmHome Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frmHome(owner);
                }
                return _obj;
            }
        }

        public Panel PnlContainer
        {
            get { return panel3; }
            set { panel3 = value; }
        }
        public Panel PnlContainer1
        {
            get { return panel6; }
            set { panel6 = value; }
        }
        
        private void HideMenu()
        {
            if (panel7.Visible == true)
            {
                panel7.Visible = false;
            }
            if (panel8.Visible == true)
            {
                panel8.Visible = false;
            }
        }
        private void ShowMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ShowMenu(panel8);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            HideMenu();
            frmHome.Instance.PnlContainer.Controls.Clear();
            NewwordImage uc2 = new NewwordImage();
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            HideMenu();
            frmHome.Instance.PnlContainer.Controls.Clear();
            ChooseStories uc2 = new ChooseStories(owner);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
          
        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            HideMenu();
            frmHome.Instance.PnlContainer.Controls.Clear();
            Videoz uc2 = new Videoz();
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowMenu(panel7);
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            HideMenu();
            panel3.Controls.Clear();
            ChooseLession uc2 = new ChooseLession(owner);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            HideMenu();
            panel3.Controls.Clear();
            ChooseTestz uc2 = new ChooseTestz(owner);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Account account = context.Accounts.Where(p => p.Account_Name == owner).SingleOrDefault();
            DialogResult a = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes &&account.Account_Type_Code==true)
            {
                this.Close();
            }
            else if( a==DialogResult.Yes && account.Account_Type_Code==false)
            {
                Application.Exit();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (!frmHome.Instance.PnlContainer.Controls.ContainsKey("Googletranslate"))
            {
                Googletranslate uc2 = new Googletranslate();
                uc2.Dock = DockStyle.Fill;
                frmHome.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmHome.Instance.PnlContainer.Controls["Googletranslate"].BringToFront();
        }
        private void frmHome_Load(object sender, EventArgs e)
        {
            _obj = this;
        }
        private void gunaCirclePictureBox1_Click(object sender, EventArgs e)
        {
            frmHome.Instance.PnlContainer.Controls.Clear();
            AccountManagerment uc2 = new AccountManagerment(owner);
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }
        private void guna2Button12_Click_1(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                frmLogin.Instance.PnlContainer1.Checked = true;
                this.Close();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmHome.Instance.PnlContainer.Controls.Clear();
            Home1 uc2 = new Home1();
            uc2.Dock = DockStyle.Fill;
            frmHome.Instance.PnlContainer.Controls.Add(uc2);
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Newfolder11.chm");
        }
    }
}
