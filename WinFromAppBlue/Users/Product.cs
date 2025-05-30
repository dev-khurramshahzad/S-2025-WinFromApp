using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFromAppBlue.Users
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            LoadData();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products", Program.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flowLayoutPanel1.Controls.Add(crpanel());
            }

        }
        Panel crpanel()
        {
            Panel p = new Panel();
            p.BackColor = Color.Indigo;
            p.BorderStyle = BorderStyle.Fixed3D;
            p.Height = 250;
            p.Width = 160;

            PictureBox pictureBox = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "team-01.png")),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 160,
                Height = 200,
                Top = 10,
                Left = 10,
            };

            p.Controls.Add(pictureBox);
            return p;
        }
    }
}
