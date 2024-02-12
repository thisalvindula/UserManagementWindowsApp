using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UserManagement
{
    public partial class Login___Skills_International : Form
    {
        public Login___Skills_International()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-9LSD45L\\SQLEXPRESS;Initial Catalog=SkillsUserManagementDb;Integrated Security=True");

            if(username.Text != "" && password.Text != "")
            {
                string query = "select count (*) from Student where username='" + username.Text + "' and " + "password='" + password.Text + "'";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int v = (int)command.ExecuteScalar();
                if(v != 1)
                {
                    MessageBox.Show("Invalid login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                } else
                {
                    Student_Registration___Skills_International registration = new Student_Registration___Skills_International();
                    this.Hide();
                    registration.Show();
                }
            } else
            {
                MessageBox.Show("Invalid login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            username.Clear();
            password.Clear();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
