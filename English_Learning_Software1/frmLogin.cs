using English_Learning_Software1.UserControlLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        static frmLogin _obj;

        public static frmLogin Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frmLogin();
                }
                return _obj;
            }
        }
        public Panel PnlContainer
        {
            get { return panel2; }
            set { panel2 = value; }
        }
        
        public RadioButton PnlContainer1
        {
            get { return radioButton1; }
            set { radioButton1 = value; }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _obj = this;
            Login uc2 = new Login();
            uc2.Dock = DockStyle.Fill;
            panel2.Controls.Add(uc2);
            
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
            }
        }
    }
}
