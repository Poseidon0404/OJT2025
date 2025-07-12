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
    public partial class Form5 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        // ID variable used in Updating and Deleting Record
        int ID = 0;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you, Admin!", "Admin", MessageBoxButtons.OK);
            var form1 = new Form1();
            form1.Show();
            Hide();
        }

        private void membersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(membersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            full_NameTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            emailTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            addressTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            phoneTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                string query = "SELECT * FROM Members WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(idTextBox.Text))
                {
                    query += " AND ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", idTextBox.Text);
                }
                if (!string.IsNullOrWhiteSpace(full_NameTextBox.Text))
                {
                    query += " AND [Full Name] LIKE @FullName";
                    cmd.Parameters.AddWithValue("@FullName", "%" + full_NameTextBox.Text + "%");
                }

                cmd.CommandText = query;

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    membersDataGridView.DataSource = dt;
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

        //delete
        private void button4_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(idTextBox.Text))
            {
                int ID;
                if (int.TryParse(idTextBox.Text, out ID)) 
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Members WHERE ID=@ID", con))
                    {
                        try
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@ID", ID);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Admin", MessageBoxButtons.OK);
                                DisplayData();
                                ClearData();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified ID.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void full_NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //for update
        private void button2_Click(object sender, EventArgs e)
        {

            if (idTextBox.Text != "" && full_NameTextBox.Text != "" && emailTextBox.Text != "" && addressTextBox.Text != "" && phoneTextBox.Text != "")
            {
                cmd = new SqlCommand("UPDATE Members SET ID=@ID, [Full Name]=@FullName, Email=@Email, Address=@Address, Phone=@Phone WHERE ID=@ID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FullName", full_NameTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", emailTextBox.Text);
                cmd.Parameters.AddWithValue("@Address", addressTextBox.Text);
                cmd.Parameters.AddWithValue("@Phone", phoneTextBox.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("The members has been updated");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please select a record to save.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear Data
        private void ClearData()
        {
            ID = 0;
            full_NameTextBox.Text = null;
            emailTextBox.Text = null;
            addressTextBox.Text = null;
            phoneTextBox.Text = null;
            databaseDataSet1 = null;
        }
        // Display Data in DataGridView
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * FROM Members", con);
            adapt.Fill(dt);
            membersDataGridView.DataSource = dt;
            con.Close();
        }

        //accounts database
        private void button3_Click(object sender, EventArgs e)
        {
            var form6 = new Form6();
            form6.Show();
            Hide();
        }

        //display button
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Datagrindview has been display/restart.", "Admin", MessageBoxButtons.OK);
            DisplayData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var form8 = new Form8();
            form8.Show();
            Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Hide Form5
            this.Hide();

            // Create and show Form2
            Form2 form2 = new Form2();
            form2.FormClosed += (s, args) =>
            {
                // When Form2 is closed, show Form5 again
                this.Show();
            };
            form2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var form6 = new Form6();
            form6.Show();
            Hide();
        }
    }
}
