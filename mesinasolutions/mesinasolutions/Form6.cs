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
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        int ID = 0;

        public Form6()
        {
            InitializeComponent();
        }

        private void accountsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accountsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet2);

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form5 = new Form5();
            form5.Show();
            Hide();
        }

        private void accountsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * FROM Accounts", con);
            adapt.Fill(dt);
            accountsDataGridView.DataSource = dt;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idTextBox.Text))
            {
                int ID;
                if (int.TryParse(idTextBox.Text, out ID))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Accounts WHERE ID=@ID", con))
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void ClearData()
        {
            ID = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
