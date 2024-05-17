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

namespace FlexTrainer
{
    public partial class Form2 : Form
    {
        Form1 prev;
        public Form2(Form1 prev)
        {
            this.prev = prev;
            InitializeComponent();
            groupBox1.BackColor = Color.FromArgb(175, Color.Black);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prev.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register form = new Register(prev);
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            SqlCommand cm;
            string un = textBox1.Text;
            string pass = textBox2.Text;

            // Check if username and password are not empty
            if (string.IsNullOrEmpty(un) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter username and password.");
                conn.Close();
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

            string query = "select typeuser from users where username = @username and password = @pass";
            cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@username", un);
            cm.Parameters.AddWithValue("@pass", pass);

            string usertype = (string)cm.ExecuteScalar();
            conn.Close();
            // Check if any rows were returned
            if (string.IsNullOrEmpty(usertype))
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }


            if (usertype == "admin")
            {
                AdminHome form = new AdminHome(un);
                this.Hide();
                form.ShowDialog();
                prev.Close();
                this.Close();
            }
            else if(usertype == "member")
            {
                MemberHome form = new MemberHome(un);
                this.Hide();
                form.ShowDialog();
                prev.Close();
                this.Close();
            }
            else if (usertype == "trainer")
            {
                TrainerHome form = new TrainerHome(un);
                this.Hide();
                form.ShowDialog();
                prev.Close();
                this.Close();
            }
            else if (usertype == "owner")
            {
                GymOwnerHome form = new GymOwnerHome(un);
                this.Hide();
                form.ShowDialog();
                prev.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }
    }
}
