using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFromAppBlue.Admin;
using WinFromAppBlue.Users;

namespace WinFromAppBlue
{
    internal static class Program
    {

        public static SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Databases\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SplashScreen s = new SplashScreen()) {
            s.ShowDialog();
            
            }

            Application.Run(new Product());
        }
    }
}
