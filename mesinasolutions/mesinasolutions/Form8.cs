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

namespace mesinasolutions
{
    public partial class Form8 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        int ID = 0;

        public Form8()
        {
            InitializeComponent();
        }

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


        // delete
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

        private void Form8_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet3.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.databaseDataSet3.Products);

        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet3);

        }

        //insert/add
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

        
        //display
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

        //update
        private void button5_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                int id;
                int quantitySold;
                decimal totalAmount;

                // Validate numeric inputs
                if (!int.TryParse(idTextBox.Text, out id))
                {
                    MessageBox.Show("Invalid ID");
                    return;
                }

                if (!int.TryParse(quantitySoldTextBox.Text, out quantitySold))
                {
                    MessageBox.Show("Invalid Quantity Sold");
                    return;
                }

                if (!decimal.TryParse(totalAmountTextBox.Text, out totalAmount))
                {
                    MessageBox.Show("Invalid Total Amount");
                    return;
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Products SET ID=@ID, CustomerName=@CustomerName, ProductName=@ProductName, QuantitySold=@QuantitySold, TotalAmount=@TotalAmount, SaleDate=@SaleDate WHERE ID=@ID", con))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = customerNameTextBox.Text;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = productNameTextBox.Text;
                    cmd.Parameters.Add("@QuantitySold", SqlDbType.Int).Value = quantitySold;
                    cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = totalAmount;
                    cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = saleDateDateTimePicker.Value;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("The Product has been updated.");
                    DisplayData();
                    ClearData();
                }
            }
            else
            {
                MessageBox.Show("Please select a record to save.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form5 = new Form5();
            form5.Show();
            Hide();
        }
    }
}
