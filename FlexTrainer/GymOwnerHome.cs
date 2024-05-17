using FlexTrainer.UserControlAdmin;
using FlexTrainer.UserControlGymOwner;
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
    public partial class GymOwnerHome : Form
    {
        string username;
        string Name;
        public GymOwnerHome(string username)
        {
            InitializeComponent();
            this.username = username;
            label2.Text = username;
            panel1.BackColor = Color.FromArgb(175, Color.Black);
            NameUpdate(username);
            OwnerHomeUC uc = new OwnerHomeUC(this.Name);
            addUserControl(uc);
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
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

            OwnerHomeUC uc = new OwnerHomeUC(this.Name);
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

            OwnerReportUC uc = new OwnerReportUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void DietButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            OwnerTrainerApprove uc = new OwnerTrainerApprove(this.username, this.Name);
            addUserControl(uc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            OwnerNewGymUC uc = new OwnerNewGymUC(this.username, this.Name);
            addUserControl(uc);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            OwnerMembers uc = new OwnerMembers(this.username, this.Name);
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
            toolTip1.SetToolTip(button3, "Manage Trainers");
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


        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button4, "Add new Gym");
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button5, "Manage Members");
        }


    }
}
