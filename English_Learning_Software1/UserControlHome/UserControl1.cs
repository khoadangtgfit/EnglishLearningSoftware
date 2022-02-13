using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class UserControl1 : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=38COMPUTER\SQLEXPRESS;Initial Catalog=English_Learning_Software;Integrated Security=True");
        public UserControl1()
        {
            InitializeComponent();
            SqlCommand cmd = new SqlCommand("insert into NewWord(Loai,Mean,MeanVN,HinhAnh) values(@loai,@mean,@meanvn,@hinh)", conn);
        }
    }
}
