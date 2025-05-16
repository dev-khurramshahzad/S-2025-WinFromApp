using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WinFromAppBlue.Admin
{
    public partial class ProductManagement : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Databases\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30");

        public string ImageName = "No Image";

        public ProductManagement()
        {
            InitializeComponent();
            LoadData();
            LoadDdls();


        }

        void LoadDdls()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Brands", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlBrand.DataSource = dt;
                ddlBrand.DisplayMember = "BrandName";
                ddlBrand.ValueMember = "BrandID";



                cmd = new SqlCommand("SELECT * FROM Categories", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                ddlCat.DataSource = dt;
                ddlCat.DisplayMember = "CatName";
                ddlCat.ValueMember = "CatID";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong Please try again later.\nErrorTrace:{ex.Message}", "Message");
            }

        }

        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT p.ProdID,p.Name,p.Image,b.BrandName,c.CatName,p.Color,p.PurchasePrice,p.SalePrice,p.Quantity FROM Products p \r\nINNER JOIN Categories c ON p.CatFID = c.CatID\r\nINNER JOIN Brands b ON b.BrandID = p.BrandFID", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Products VALUES ('{txtName.Text}','{txtDetail.Text}','{ImageName}','{txtSPrice.Text}','{txtCPrice.Text}','{txtColor.Text}','{txtSize.Text}','{txtQuantity.Text}','{ddlStatus.SelectedItem.ToString()}',NULL,NULL,'{ddlBrand.SelectedValue}','{ddlCat.SelectedValue}')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                LoadData();


                txtName.Text = "";
                txtDetail.Text = "";
                MessageBox.Show($"{txtName.Text} is added", "Data Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Somthing went wrong, Please try again\nTrace: {ex.Message}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }


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

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Products Where ProdID = '{txtID.Text}'", con);
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
                    cmd = new SqlCommand($"DELETE FROM Products Where ProdID = '{txtID.Text}'", con);
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

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Products Where ProdID = '{txtID.Text}'", con);
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
                    SqlCommand cmd = new SqlCommand($"UPDATE Products SET Name = '{txtName.Text}', Details = '{txtDetail.Text}' Where ProdID = '{txtID.Text}'", con);
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.png;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImgPreview.Image = Image.FromFile(ofd.FileName);

                ImageName = Path.GetFileName(ofd.FileName);
                lblImageName.Text = ImageName;

                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), ImageName);

                File.Copy(ofd.FileName, SavePath,true);

            }





            ;


        }
    }
}