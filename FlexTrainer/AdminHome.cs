using FlexTrainer.UserControlAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer
{
    public partial class AdminHome : Form
    {
        string username;
        string Name;
        public AdminHome(string username)
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(175, Color.Black);
            this.username = username;
            label2.Text = username;
            NameUpdate(username);
            AdminHomeUC uc = new AdminHomeUC(this.Name);
            addUserControl(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void NameUpdate(string username)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            SqlCommand cm;

            string query = "select Name from users where username = @username";
            cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@username", username);

            string Name = (string)cm.ExecuteScalar();
            conn.Close();

            this.Name = Name;
            }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelcontainer.Controls.Clear();
            panelcontainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void Home_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            AdminHomeUC uc = new AdminHomeUC(this.Name);
            addUserControl(uc);

            /*  MemberHomeForm h = new MemberHomeForm();
        h.TopLevel = false;
        if(panel2.Controls.Count > 0)
        {
            panel2.Controls.Clear();
        }
        panel2.Controls.Add(h);
        h.BringToFront();
        h.Show();*/


        }


        private void WorkoutButton_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            AdminGymReportUC uc = new AdminGymReportUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void DietButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            AdminGymRegisterUC uc = new AdminGymRegisterUC(this.username, this.Name);
            addUserControl(uc);
        }



        private void Home_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(Home, "Home");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button2, "Performance Reports");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "Gym View");
        }


        private void logout_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(logout, "LogOut");
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
        }


    }
}
