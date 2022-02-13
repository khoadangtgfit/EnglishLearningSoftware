using English_Learning_Software1.Models;
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

namespace English_Learning_Software1.UserControlAdmin
{
    public partial class Report : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        SqlConnection conn = new SqlConnection(@"Data Source=38COMPUTER\SQLEXPRESS;Initial Catalog=English_Learning_Software;Integrated Security=True");
        public Report()
        {
            InitializeComponent();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from Ownere", conn);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds, "Ownere");
            CrystalReport2 cr1 = new CrystalReport2();
            cr1.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr1;
            crystalReportViewer1.Refresh();
            conn.Close();
        }
        

        
    }
}
