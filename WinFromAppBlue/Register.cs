using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFromAppBlue
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Required Field
            if(txtName.Text=="" || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please Fill All Required Fields Properly", "Message");
                return;
            }

            //Password
            if (txtPass.Text != txtCPass.Text)
            {
                MessageBox.Show("Password do not match please try again", "Message");
                return;
            }

            //Email Format
            if (!Regex.IsMatch(txtEmail.Text, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
            {
                MessageBox.Show("Please Enter a Valid Email.", "Message");
                return;
            }

            //Password Valida
            if (!Regex.IsMatch(txtPass.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
            {
                MessageBox.Show("Week Password, Password must contain an Uppercase, Lower, Number and Spacial Symbol, must be 8 character long", "Message");
                return;
            }




            //Database Insert

            //Create Connection
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Khurram-Mac\\Desktop\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30;");

            //Creating Command
            SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES ('"+txtName.Text+"','"+txtEmail.Text+"','"+txtPass.Text+"','"+txtAddress.Text+"','"+txtPhone.Text+"','Image','N-A','Active')", con);
            //Opening Connection
            con.Open();
            //Executing Query
            cmd.ExecuteNonQuery();
            //Closing Connection
            con.Close();


            //Show Message
            MessageBox.Show("Account Registered","Message");
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtPass.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
            {
                lblPassVali.Text = "Weak Password";
            }
            else
            {
                lblPassVali.Text = "Strong Password";
                lblPassVali.BackColor = Color.Green;
            }

        }
    }
}
