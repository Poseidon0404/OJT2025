using mesinasolutions;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mesinasolutions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Hardcoded admin login
            if (username == "admin" && password == "admin")
            {
                var form2 = new Form2();
                form2.Show();
                this.Hide();
                return;
            }

            // Check from database
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\mesinasolutions\\mesinasolutions\\Database.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Accounts WHERE Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        var form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect username or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form3 = new Form3();
            form3.Show();
            Hide();
            MessageBox.Show("Welcome to Admin Menu", "Admin", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form4 = new Form4();
            form4.Show();
            Hide();
            MessageBox.Show("Welcome to Register Menu", "Register", MessageBoxButtons.OK);
        }
    }
}


