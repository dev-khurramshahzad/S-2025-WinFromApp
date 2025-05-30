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
    public partial class Brands : Form
    {
        public Brands()
        {
            InitializeComponent();
            LoadData();
        }


        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Brands", Program.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flowLayoutPanel1.Controls.Add(CreatePanel(dt.Rows[i][3].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()));
                
            }


        }

        private Panel CreatePanel(string pictiure, string name, string details)
        {
            Panel panel = new Panel()
            {
                BackColor = Color.Pink,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 250,
                Width = 150,

            };



            PictureBox pictureBox = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "team-01.png")),
                Height = 150,
                Width = 120,
                Top = 5,
                Left = 10,
                SizeMode = PictureBoxSizeMode.StretchImage

            };

            Label label = new Label()
            {
                Text = name,
                Height = 15,
                Width = 120,
                Top = 160,
                Left = 5,

            };

            Label label2 = new Label()
            {
                Text = details,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 120,
                Height = 15,
                Top = 180,
                Left = 5,

            };

            Button button = new Button()
            {
                Text = "View Products",
                Width = 120,
                Height = 25,
                Top = 200,
                Left = 5,

            };

            button.Click += (sender,id) =>
            {
                MessageBox.Show("You Clicked this Button");
            };






            panel.Controls.Add(pictureBox);
            panel.Controls.Add(label);
            panel.Controls.Add(label2);
            panel.Controls.Add(button);

            return panel;
          
        }

       
    }


}
