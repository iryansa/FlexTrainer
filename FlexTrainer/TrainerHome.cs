using FlexTrainer.UserControlsTrainer;
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
    public partial class TrainerHome : Form
    {
        string username;
        string Name;
        public TrainerHome(string username)
        {
            InitializeComponent();
            this.username = username;
            panel1.BackColor = Color.FromArgb(175, Color.Black);
            label2.Text = username;
            NameUpdate(username);
            TrainerHomeUC uc = new TrainerHomeUC(this.Name, this.username);
            addUserControl(uc);
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query1 = "select approved from trainergym where username = '" + this.username + "'";
            SqlCommand cmd = new SqlCommand(query1, conn);
            string approval = (string)cmd.ExecuteScalar();
            if(string.IsNullOrEmpty(approval) || approval == "Unapproved")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }

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

            TrainerHomeUC uc = new TrainerHomeUC(this.Name, this.username);
            addUserControl(uc);

                /*  TrainerHomeForm h = new TrainerHomeForm();
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

            TrainerWorkoutPlanUC uc = new TrainerWorkoutPlanUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void DietButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            TrainerDiettPlanUC uc = new TrainerDiettPlanUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void TrainerButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            TrainerBookingUC uc = new TrainerBookingUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void FeedbackButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panel3.Location = new Point(button.Location.X, button.Location.Y);

            TrainerFeedBackUC uc = new TrainerFeedBackUC(this.username, this.Name);
            addUserControl(uc);
        }

        private void Home_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(Home, "Home");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button2, "Workout Plans");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "Diet Plans");
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button4, "Manage Your Sessions");
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button5, "View Your Ratings");
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void logout_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(logout, "Log Out");
        }
    }
}
