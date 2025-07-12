using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mesinasolutions
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if((textBox1.Text == "admin") && (textBox2.Text == "admin"))
            {
                MessageBox.Show("Login as Admin", "Admin", MessageBoxButtons.OK);
                var form5 = new Form5();
                form5.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("You are not Admin!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
