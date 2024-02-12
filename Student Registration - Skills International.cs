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
    public partial class Student_Registration___Skills_International : Form
    {
        public Student_Registration___Skills_International()
        {
            InitializeComponent();
            PopulateRegNosComboBox();
            regNoText.SelectedIndexChanged += regNoText_SelectedIndexChanged;
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-9LSD45L\\SQLEXPRESS;Initial Catalog=SkillsUserManagementDb;Integrated Security=True");
        private void registerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (regNoText.Text != "" && fNameText.Text != "" && lNameText.Text != "" && dobPicker.Text != "Monday, January 1, 1753" && (maleRadio.Checked || femaleRadio.Checked) && addressText.Text != "" &&
                    emailText.Text != "" && mobileText.Text != "" && homePhoneText.Text != "" && pNameText.Text != "" && pNicText.Text != "" && pContactText.Text != "")
                {
                    int v = check(int.Parse(regNoText.Text));
                    if (v == 0)
                    {
                        if (maleRadio.Checked)
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("insert into Registration values(@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)", connection);
                            command.Parameters.AddWithValue("@regNo", regNoText.Text);
                            command.Parameters.AddWithValue("@firstName", fNameText.Text);
                            command.Parameters.AddWithValue("@lastName", lNameText.Text);
                            command.Parameters.AddWithValue("@dateOfBirth", Convert.ToDateTime(dobPicker.Text));
                            command.Parameters.AddWithValue("@gender", "Male");
                            command.Parameters.AddWithValue("@address", addressText.Text);
                            command.Parameters.AddWithValue("@email", emailText.Text);
                            command.Parameters.AddWithValue("@mobilePhone", mobileText.Text);
                            command.Parameters.AddWithValue("@homePhone", homePhoneText.Text);
                            command.Parameters.AddWithValue("@parentName", pNameText.Text);
                            command.Parameters.AddWithValue("@nic", pNicText.Text);
                            command.Parameters.AddWithValue("@contactNo", pContactText.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Record Added Successfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("insert into Registration values(@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)", connection);
                            command.Parameters.AddWithValue("@regNo", regNoText.Text);
                            command.Parameters.AddWithValue("@firstName", fNameText.Text);
                            command.Parameters.AddWithValue("@lastName", lNameText.Text);
                            command.Parameters.AddWithValue("@dateOfBirth", Convert.ToDateTime(dobPicker.Text));
                            command.Parameters.AddWithValue("@gender", "Female");
                            command.Parameters.AddWithValue("@address", addressText.Text);
                            command.Parameters.AddWithValue("@email", emailText.Text);
                            command.Parameters.AddWithValue("@mobilePhone", mobileText.Text);
                            command.Parameters.AddWithValue("@homePhone", homePhoneText.Text);
                            command.Parameters.AddWithValue("@parentName", pNameText.Text);
                            command.Parameters.AddWithValue("@nic", pNicText.Text);
                            command.Parameters.AddWithValue("@contactNo", pContactText.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Record Added Successfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        regNoText.Items.Clear();
                        PopulateRegNosComboBox();
                    }
                    else
                    {
                        MessageBox.Show("This student already exist", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fill all fields", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        int check(int regNo)
        {
            connection.Open();
            string query = "select count(*) from Registration where regNo='" + regNo + "'";
            SqlCommand command = new SqlCommand(query, connection);
            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v;
        }

        private void PopulateRegNosComboBox()
        {
            try
            {
                connection.Open();
                string query = "SELECT regNo FROM Registration";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string regNo = reader["regNo"].ToString();
                        regNoText.Items.Add(regNo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void regNoText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regNoText.SelectedItem != null)
            {
                string selectedRegNo = regNoText.SelectedItem.ToString();
                update_form_fields(selectedRegNo);
            }
        }

        private void update_form_fields(String  regNo)
        {
            try
            {
                string selectedRegNo = regNoText.SelectedItem.ToString();
                connection.Open();
                string query = "SELECT * FROM Registration WHERE regNo = @regNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@regNo", selectedRegNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fNameText.Text = reader["firstName"].ToString();
                        lNameText.Text = reader["lastName"].ToString();
                        dobPicker.Value = Convert.ToDateTime(reader["dateOfBirth"]);
                        string gender = reader["gender"].ToString();
                        maleRadio.Checked = (gender == "Male");
                        femaleRadio.Checked = (gender == "Female");
                        addressText.Text = reader["address"].ToString();
                        emailText.Text = reader["email"].ToString();
                        mobileText.Text = reader["mobilePhone"].ToString();
                        homePhoneText.Text = reader["homePhone"].ToString();
                        pNameText.Text = reader["parentName"].ToString();
                        pNicText.Text = reader["nic"].ToString();
                        pContactText.Text = reader["contactNo"].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Student");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (regNoText.Text != "" && fNameText.Text != "" && lNameText.Text != "" && dobPicker.Text != "Monday, January 1, 1753" && (maleRadio.Checked || femaleRadio.Checked) && addressText.Text != "" &&
                    emailText.Text != "" && mobileText.Text != "" && homePhoneText.Text != "" && pNameText.Text != "" && pNicText.Text != "" && pContactText.Text != "")
                {
                    int v = check(int.Parse(regNoText.Text));
                    if (v == 1)
                    {
                        if (maleRadio.Checked)
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("UPDATE Registration SET " +
                                   "firstName = @firstName, " +
                                   "lastName = @lastName, " +
                                   "dateOfBirth = @dateOfBirth, " +
                                   "gender = @gender, " +
                                   "address = @address, " +
                                   "email = @email, " +
                                   "mobilePhone = @mobilePhone, " +
                                   "homePhone = @homePhone, " +
                                   "parentName = @parentName, " +
                                   "nic = @nic, " +
                                   "contactNo = @contactNo " +
                                   "WHERE regNo = @regNo", connection);

                            command.Parameters.AddWithValue("@regNo", regNoText.Text);
                            command.Parameters.AddWithValue("@firstName", fNameText.Text);
                            command.Parameters.AddWithValue("@lastName", lNameText.Text);
                            command.Parameters.AddWithValue("@dateOfBirth", Convert.ToDateTime(dobPicker.Text));
                            command.Parameters.AddWithValue("@gender", "Male");
                            command.Parameters.AddWithValue("@address", addressText.Text);
                            command.Parameters.AddWithValue("@email", emailText.Text);
                            command.Parameters.AddWithValue("@mobilePhone", mobileText.Text);
                            command.Parameters.AddWithValue("@homePhone", homePhoneText.Text);
                            command.Parameters.AddWithValue("@parentName", pNameText.Text);
                            command.Parameters.AddWithValue("@nic", pNicText.Text);
                            command.Parameters.AddWithValue("@contactNo", pContactText.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Record Updated Successfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("UPDATE Registration SET " +
                                   "firstName = @firstName, " +
                                   "lastName = @lastName, " +
                                   "dateOfBirth = @dateOfBirth, " +
                                   "gender = @gender, " +
                                   "address = @address, " +
                                   "email = @email, " +
                                   "mobilePhone = @mobilePhone, " +
                                   "homePhone = @homePhone, " +
                                   "parentName = @parentName, " +
                                   "nic = @nic, " +
                                   "contactNo = @contactNo " +
                                   "WHERE regNo = @regNo", connection);

                            command.Parameters.AddWithValue("@regNo", regNoText.Text);
                            command.Parameters.AddWithValue("@firstName", fNameText.Text);
                            command.Parameters.AddWithValue("@lastName", lNameText.Text);
                            command.Parameters.AddWithValue("@dateOfBirth", Convert.ToDateTime(dobPicker.Text));
                            command.Parameters.AddWithValue("@gender", "Female");
                            command.Parameters.AddWithValue("@address", addressText.Text);
                            command.Parameters.AddWithValue("@email", emailText.Text);
                            command.Parameters.AddWithValue("@mobilePhone", mobileText.Text);
                            command.Parameters.AddWithValue("@homePhone", homePhoneText.Text);
                            command.Parameters.AddWithValue("@parentName", pNameText.Text);
                            command.Parameters.AddWithValue("@nic", pNicText.Text);
                            command.Parameters.AddWithValue("@contactNo", pContactText.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Record Updated Successfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No any record to update of this student", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fill all fields", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Student");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (regNoText.SelectedItem != null)
                {
                    string selectedRegNo = regNoText.SelectedItem.ToString();

                    // Show a confirmation dialog
                    DialogResult result = MessageBox.Show("Are you sure? Do you really want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("DELETE FROM Registration WHERE regNo = @regNo", connection);
                        command.Parameters.AddWithValue("@regNo", selectedRegNo);
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record Deleted Successfully", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            regNoText.Items.Clear();
                            PopulateRegNosComboBox();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete record", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select a student to delete", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Delete Student");
            }
        }

        private void ClearForm()
        {
            // Clear all form fields
            regNoText.SelectedIndex = -1;
            fNameText.Clear();
            lNameText.Clear();
            dobPicker.Value = DateTime.Now;
            maleRadio.Checked = false;
            femaleRadio.Checked = false;
            addressText.Clear();
            emailText.Clear();
            mobileText.Clear();
            homePhoneText.Clear();
            pNameText.Clear();
            pNicText.Clear();
            pContactText.Clear();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void exitButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logoutButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login___Skills_International login = new Login___Skills_International();
            this.Hide();
            login.Show();
        }
    }
}
