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

namespace WinFromAppBlue.Admin
{
    public partial class CategoryManagement : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Databases\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30");

        public CategoryManagement()
        {
            InitializeComponent();
            LoadData();


        }

        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO Categories VALUES ('{txtName.Text}','{txtDetail.Text}','Image Path','{ddlStatus.SelectedItem.ToString()}')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadData();


            txtName.Text = "";
            txtDetail.Text = "";
            MessageBox.Show($"{txtName.Text} is added", "Data Added", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    MessageBox.Show("Please Enter ID to delete that record", "ID Not Entered");
                    return;
                }

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Categories Where CatID = '{txtID.Text}'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show($"The {txtID.Text} doesn't match to any of the record please try again", "ID Not Matched");
                    return;
                }

                var respose = MessageBox.Show("Are you sure you want to delete that item", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (respose == DialogResult.Yes)
                {
                    cmd = new SqlCommand($"DELETE FROM Categories Where CatID = '{txtID.Text}'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("This item has been deleted.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                LoadData();



            }
            catch (Exception ex)
            {

                MessageBox.Show($"Somthing went wrong, Please try again\nTrace: {ex.Message}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }





        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please Enter ID to delete that record", "ID Not Entered");
                return;
            }

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Categories Where CatID = '{txtID.Text}'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show($"The {txtID.Text} doesn't match to any of the record please try again", "ID Not Matched");
                return;
            }

            txtName.Text = dt.Rows[0][1].ToString();
            txtDetail.Text = dt.Rows[0][2].ToString();

            btnEdit.Visible = false;
            btnUpdate.Visible = true;

            txtID.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnView.Enabled = false;




        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var respose = MessageBox.Show("Are you sure you want to update that item", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (respose == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE Categories SET CatName = '{txtName.Text}', Details = '{txtDetail.Text}' Where CatID = '{txtID.Text}'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("This item has been Updated.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                LoadData();


                txtName.Text = "";
                txtDetail.Text = "";
                txtID.Text = "";

                btnEdit.Visible = true;
                btnUpdate.Visible = false;

                txtID.Enabled = true;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnView.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Somthing went wrong, Please try again\nTrace: {ex.Message}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }


        }
    }
}
