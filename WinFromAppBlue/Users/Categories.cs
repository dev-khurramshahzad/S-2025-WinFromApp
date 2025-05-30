using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFromAppBlue.Users
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", Program.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flowLayoutPanel1.Controls.Add(CreatePanel(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()));
            }


        }

        Panel CreatePanel(string name,string details,string imagePath)
        {
            Panel panel = new Panel()
            {
                Width = 150,
                Height = 250,
                BackColor = Color.SkyBlue,
            };

            PictureBox pictureBox = new PictureBox()
            {
                Height = 150,
                Width = 130,
                Top = 10,
                Left = 10,

                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "team-01.png"))
            };

            Label label = new Label()
            {
                Height = 20,
                Width = 130,
                Top = 170,
                Left = 10,

                Text = name,
                ForeColor = Color.White,
            };

            Label label2 = new Label()
            {
                Height = 20,
                Width = 130,
                Top = 190,
                Left = 10,

                Text = details,
                ForeColor = Color.White,
            };

            Button button = new Button()
            {
                Text = "View Products",
                Height = 30,
                Width = 130,
                Top = 220,
                Left = 10,

            };
            button.Click += (sender, e) =>
            {
                MessageBox.Show(label.Text);
            };


            panel.Controls.Add(pictureBox); 
            panel.Controls.Add(label); 
            panel.Controls.Add(label2); 
            panel.Controls.Add(button); 
            


            return panel;
        }


    }
}
