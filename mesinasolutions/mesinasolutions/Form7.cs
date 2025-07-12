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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mesinasolutions
{
    public partial class Form7 : Form
    {

        public  SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True");
        public SqlCommand cmd;
        public SqlDataAdapter adapt;

        // ID variable used in Updating and Deleting Record
        int ID = 0;

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        { 
            // TODO: This line of code loads data into the 'databaseDataSet3.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.databaseDataSet3.Products);
        }

        //insert
        private void button6_Click(object sender, EventArgs e)
        {

            if (idTextBox.Text != "" && customerNameTextBox.Text != "" && productNameTextBox.Text != "" && quantitySoldTextBox.Text != "" && totalAmountTextBox.Text != "")
            {
                cmd = new SqlCommand("INSERT INTO Products (ID, CustomerName, ProductName, QuantitySold, TotalAmount, SaleDate) VALUES (@ID, @CustomerName, @ProductName, @QuantitySold, @TotalAmount, @SaleDate)", con);
                con.Open();             
                cmd.Parameters.AddWithValue("@ID", idTextBox.Text);
                cmd.Parameters.AddWithValue("@CustomerName", customerNameTextBox.Text);
                cmd.Parameters.AddWithValue("@ProductName", productNameTextBox.Text);
                cmd.Parameters.AddWithValue("@QuantitySold", quantitySoldTextBox.Text);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmountTextBox.Text);
                cmd.Parameters.AddWithValue("@SaleDate", saleDateDateTimePicker.Value);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Products inserted successfully!", "Admin", MessageBoxButtons.OK);
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please input your credentials", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear Data
        private void ClearData()
        {
            ID = 0;
            customerNameTextBox.Text = null;
            productNameTextBox.Text = null;
            quantitySoldTextBox.Text = null;
            totalAmountTextBox.Text = null;
            databaseDataSet3 = null;

        }
        // Display Data in DataGridView
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * FROM Products", con);
            adapt.Fill(dt);
            productsDataGridView.DataSource = dt;
            con.Close();
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void productNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void saleDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void customerNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalAmountTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //display/restart
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Datagrindview has been display/restart.", "", MessageBoxButtons.OK);
            DisplayData();
        }

        //search
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                string query = "SELECT * FROM Products WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(idTextBox.Text))
                {
                    query += " AND ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", idTextBox.Text);
                }
                if (!string.IsNullOrWhiteSpace(customerNameTextBox.Text))
                {
                    query += " AND [Customer Name] LIKE @CustomerName";
                    cmd.Parameters.AddWithValue("@CustomerName", "%" + customerNameTextBox.Text + "%");
                }

                cmd.CommandText = query;

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    productsDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        //deleted
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idTextBox.Text))
            {
                int ID;
                if (int.TryParse(idTextBox.Text, out ID))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ID=@ID", con))
                    {
                        try
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@ID", ID);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Products deleted successfully!", "", MessageBoxButtons.OK);
                                DisplayData();
                                ClearData();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid ID. Please enter a valid number.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
