using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer
{
    public partial class OwnerRegister : Form
    {
        Register prev;
        Form1 form1;
        public OwnerRegister(Register prev, Form1 form1)
        {
            InitializeComponent();
            this.prev = prev;
            groupBox1.BackColor = Color.FromArgb(175, Color.Black);
            this.form1 = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.Close();  
            prev.Close();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            SqlCommand cm;
            string un = textBox3.Text;
            string pass = textBox2.Text;
            string name = textBox1.Text;
            string email = textBox4.Text;

            if (string.IsNullOrEmpty(un) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill out all the details");
                return;
            }

            if (un.Length < 5 || un.Length > 15)
            {
                MessageBox.Show("Username must be at least 5 characters and at most 15 characters long");
                conn.Close();
                return;
            }
            if (pass.Length < 5 || pass.Length > 15)
            {
                MessageBox.Show("Password must be at least 5 characters and at most 15 characters long");
                conn.Close();
                return;
            }

            string englishAlphabetPattern = @"^[a-zA-Z\s]+$";
            Regex regex = new Regex(englishAlphabetPattern);
            bool isValidName = regex.IsMatch(name);



            if (!isValidName)
            {
                MessageBox.Show("Name can only include valid charcters [A/a to Z/z]");
                conn.Close();
                return;
            }

            try
            {
                new MailAddress(email);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Email");
                conn.Close();
                return;
            }

            string query = "insert into users(username, name, email, password, typeuser) values ('" + un + "', '" + name + "', '" + email + "', '" + pass + "', 'owner');";


            string query1 = "select typeuser from users where username = @username";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            cm1.Parameters.AddWithValue("@username", un);

            string usertype = (string)cm1.ExecuteScalar();
            if (!string.IsNullOrEmpty(usertype))
            {
                MessageBox.Show("Username already exists");
                conn.Close();
                return;
            }

            cm = new SqlCommand(query, conn);
            cm.ExecuteNonQuery();
            cm.Dispose();
            conn.Close();

            GymOwnerHome form = new GymOwnerHome(un);
            this.Hide();
            form.ShowDialog();
            prev.Close();
            form1.Close();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }
    }
}
