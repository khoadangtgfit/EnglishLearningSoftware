using English_Learning_Software1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (Properties.Settings.Default.Rememberme == true)
            //{
            //Application.Run(new frmHome(Properties.Settings.Default.Username));
            //}
            //else
            //{
            Application.Run(new frmLogin());
        //}
    }
    }
}
