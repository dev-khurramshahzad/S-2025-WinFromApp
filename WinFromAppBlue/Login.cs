using Microsoft.Win32;
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

namespace WinFromAppBlue
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Khurram-Mac\\Desktop\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30;");

        public Login()
        {
            InitializeComponent();
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * FROM Users Where Email='"+txtEmail.Text+"' AND Password = '"+txtPassword.Text+"'",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Form1 f = new Form1();
                f.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Email or Password ", "Error");
            }

        }
    }
}
