using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace FlexTrainer
{
    public partial class WorkoutCreate : Form
    {
 

        string username;
        string[] muscle = new string[8];
        string[] machine = new string[8];
        string[] exercise = new string[8];
        int[] sets = new int[8];
        int[] reps = new int[8];
        int index;
        public WorkoutCreate(string username)
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(150, Color.Black);
            this.username = username;
            index = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string workoutname = (string)textBox1.Text;
            string goal = comboBox2.Text;
            string explevel = comboBox1.Text;
            string schedule = comboBox3.Text;
            string authortype = "member";
            string author = this.username;

            if (string.IsNullOrEmpty(workoutname) || string.IsNullOrEmpty(goal) || string.IsNullOrEmpty(explevel) || string.IsNullOrEmpty(schedule))
            {
                MessageBox.Show("Please fill out all the details");
                return;
            }
            if (string.IsNullOrEmpty(muscle[7]))
            {
                MessageBox.Show("Please fill out all the details for all the 7 days.");
                return;
            }
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();


            string query = "insert into workoutPlan(workout_name, goal, exp_level , author, authortype, schedule) values ('" + workoutname + "', '" + goal + "', '" + explevel + "', '" + author + "', 'member', '" + schedule + "'); ";
            SqlCommand cm = new SqlCommand(query, conn);

            cm.ExecuteNonQuery();
            cm.Dispose();




            string query1 = "DECLARE @current INT  = (SELECT SCOPE_IDENTITY()); insert into excercise(workoutday, workoutId, exercisename, machine, sets, reps, musclegroup) values (1 , @current, '" + exercise[1] + "' , '" + machine[1] + "' , " + sets[1].ToString() + " , " + reps[1].ToString() + " , '" + muscle[1] + "'), (2 , @current, '" + exercise[2] + "' , '" + machine[2] + "' , " + sets[2].ToString() + " , " + reps[2].ToString() + " , '" + muscle[2] + "') , (3 , @current, '" + exercise[3] + "' , '" + machine[3] + "' , " + sets[3].ToString() + " , " + reps[3].ToString() + " , '" + muscle[3] + "') , (4 , @current, '" + exercise[4] + "' , '" + machine[4] + "' , " + sets[4].ToString() + " , " + reps[4].ToString() + " , '" + muscle[4] + "') , (5 , @current, '" + exercise[5] + "' , '" + machine[5] + "' , " + sets[5].ToString() + " , " + reps[5].ToString() + " , '" + muscle[5] + "') , (6 , @current, '" + exercise[6] + "' , '" + machine[6] + "' , " + sets[6].ToString() + " , " + reps[6].ToString() + " , '" + muscle[6] + "') , (7 , @current, '" + exercise[7] + "' , '" + machine[7] + "' , " + sets[7].ToString() + " , " + reps[7].ToString() + " , '" + muscle[7] + "');";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            cm1.ExecuteNonQuery();
            cm1.Dispose();
            conn.Close();


            MessageBox.Show("Workout Addition Successfull");
            this.Close();
        }

        private void btnAddDay_Click(object sender, EventArgs e)
        {
            if (this.index == 7)
            {
                MessageBox.Show("All days have already been added!");
                return;
            }
            if (string.IsNullOrEmpty(txtMuscleGroupX.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill out all the details for the current day.");
                return;
            }
            this.index++;



            string musclegroup = (string)txtMuscleGroupX.Text;
            string machinename = (string)textBox4.Text;
            string excercisename = (string)textBox2.Text;

            int setsnum = int.Parse(textBox3.Text);
            int repsnum = int.Parse(textBox5.Text);

            this.muscle[this.index] = musclegroup;
            this.exercise[this.index] = excercisename;
            this.machine[this.index] = machinename;
            this.sets[this.index] = setsnum;
            this.reps[this.index] = repsnum;

            label12.Text = (this.index + 1).ToString();

            txtMuscleGroupX.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();

            if (this.index == 7)
            {
                MessageBox.Show("All days added!");
                txtMuscleGroupX.Enabled = false;
                textBox4.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox5.Enabled = false;
                return;
            }


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
