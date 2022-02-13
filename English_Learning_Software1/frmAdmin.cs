using English_Learning_Software1.Models;
using English_Learning_Software1.UserControlAdmin;
using English_Learning_Software1.UserControlHome;
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

namespace English_Learning_Software1
{
    public partial class frmAdmin : Form
    {
        static string owner;
        public frmAdmin(string name)
        {
            InitializeComponent();
            owner = name;
            Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == owner);
            Ownere owner1 = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
            label3.Text = owner1.Ownere_FullName;
            if (owner1.Ownere_Image == null)
            {

            }
            else
            {
                gunaCirclePictureBox1.Image = ConvertBinaryToImage(owner1.Ownere_Image);
            }
        }
        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Image.FromStream(memoryStream);
            }
        }
        EnglishDBContext context =new EnglishDBContext();
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        static frmAdmin _obj;
        public static frmAdmin Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frmAdmin(owner);
                }
                return _obj;
            }
        }
        public Panel PnlContainer
        {
            get { return panel3; }
            set { panel3 = value; }
        }
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("NewwordManagerment"))
            {
                NewwordManagerment uc2 = new NewwordManagerment();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["NewwordManagerment"].BringToFront();
        }
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            _obj = this;
            Home uc3 = new Home();
            uc3.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc3);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("Home"))
            {
                Home uc2 = new Home();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["Home"].BringToFront();
        }
        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("UserManagerment"))
            {
                UserManagerment uc2 = new UserManagerment();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["UserManagerment"].BringToFront();
        }
        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("VideoManagerment"))
            {
                VideoManagerment uc2 = new VideoManagerment();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["VideoManagerment"].BringToFront();
        }
        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("StoriesManagerment"))
            {
                StoriesManagerment uc2 = new StoriesManagerment();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["StoriesManagerment"].BringToFront();
        }
        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            if (!frmAdmin.Instance.PnlContainer.Controls.ContainsKey("TestzManagerment"))
            {
                TestzManagerment uc2 = new TestzManagerment();
                uc2.Dock = DockStyle.Fill;
                frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
            }
            frmAdmin.Instance.PnlContainer.Controls["TestzManagerment"].BringToFront();
        }
        private void gunaCirclePictureBox1_Click(object sender, EventArgs e)
        {
            frmAdmin.Instance.PnlContainer.Controls.Clear();
            AccountManagerment uc2 = new AccountManagerment(owner);
            uc2.Dock = DockStyle.Fill;
            frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                frmLogin.Instance.PnlContainer1.Checked = true;
                this.Close();
            }
        }
        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            frmAdmin.Instance.PnlContainer.Controls.Clear();
            Chart uc2 = new Chart();
            uc2.Dock = DockStyle.Fill;
            frmAdmin.Instance.PnlContainer.Controls.Add(uc2);
        }
        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Phan Mem Hoc Tieng Anh HKT.chm");
        }
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome(owner);
            frmHome.Show();
        }
    }
}
