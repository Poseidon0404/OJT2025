using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace mesinasolutions
{
    public partial class Form2 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        // ID variable used in Updating and Deleting Record
        int ID = 0;

        public Form2()
        {
            InitializeComponent();
        }


        private void membersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.membersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet1);

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void phoneLabel_Click(object sender, EventArgs e)
        {

        }

        private void phoneTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void membersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(membersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            full_NameTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            emailTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            addressTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            phoneTextBox.Text = membersDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.Show();
            Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {

            if (idTextBox.Text != "" && full_NameTextBox.Text != "" && emailTextBox.Text != "" && addressTextBox.Text != "" && phoneTextBox.Text != "")
            {
                cmd = new SqlCommand("INSERT INTO Members (ID, [Full Name], Email, Address, Phone) VALUES (@ID, @FullName, @Email, @Address, @Phone)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", idTextBox.Text);
                cmd.Parameters.AddWithValue("@FullName", full_NameTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", emailTextBox.Text);
                cmd.Parameters.AddWithValue("@Address", addressTextBox.Text);
                cmd.Parameters.AddWithValue("@Phone", phoneTextBox.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record inserted successfully!", "Admin", MessageBoxButtons.OK);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (ID != 0)
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
                MessageBox.Show("The account has been saved in the database.");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please select a record to save.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Datagrindview has been display/restart.", "", MessageBoxButtons.OK);
            DisplayData();
        }

        private void idLabel_Click(object sender, EventArgs e)
        {

        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addressLabel_Click(object sender, EventArgs e)
        {

        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailLabel_Click(object sender, EventArgs e)
        {

        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void full_NameLabel_Click(object sender, EventArgs e)
        {

        }

        private void full_NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enjoy Shopping!", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            var form7 = new Form7();
            form7.Show();
            Hide();
        }
    }
}
