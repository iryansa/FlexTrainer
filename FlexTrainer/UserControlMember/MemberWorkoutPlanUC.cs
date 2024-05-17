using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer.UserControlsMember
{
    public partial class MemberWorkoutPlanUC : UserControl
    {
        string username, Name;
        public MemberWorkoutPlanUC(string username, string Name)
        {
            InitializeComponent();
            groupBox1.BackColor = Color.FromArgb(50, Color.Black);
            this.BackColor = Color.FromArgb(50, Color.Black);
            this.username = username;
            this.Name = Name;
            label2.Text = Name;
            radioButton1.Checked = true;
            tableRefresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkoutCreate workoutCreate = new WorkoutCreate(this.username);
            workoutCreate.ShowDialog();
            tableRefresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string radio = "";
            string exp = "";
            string sched = "";
            string sname = "";
            string sid = "";

            if (radioButton2.Checked)
            {
                radio = "and authortype = 'trainer' ";
            }
            if(radioButton3.Checked) {
                radio = "and authortype = 'member' ";
            }

            if(!string.IsNullOrEmpty(comboBox1.Text))
            {
                exp = "and exp_level = '" + comboBox1.Text + "' ";
            }

            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                sched = "and schedule = '" + comboBox2.Text + "' ";
            }
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                sname = "and workout_name = '" + textBox1.Text + "' ";
            }
            if(!string.IsNullOrEmpty(textBox2.Text))
            {
                sid = "and workout_id = " + textBox2.Text + " ";
            }

            string query = "select * from workoutPlan where workout_id > 0 " + radio + exp + sched + sname + sid;

            DataTable dt = new DataTable();
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();

            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            




        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);   
            showworkout_diet form = new showworkout_diet(id, this.username, "FlexTrainer - Workout Plan Info");
            form.ShowDialog();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                        //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            // first check if the trainer has already created a slot with the same timestamp
            string query1 = "Select workoutid from memberworkout where memberusername like '" + this.username + "' ";
            SqlCommand cm = new SqlCommand(query1, conn);

            bool flag = false;
            int id1 = 0;
            using (SqlDataReader reader = cm.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        id1 = reader.GetInt32(0);
                        flag = true;
                    }
                }

            }
            if(flag) { 

                string query2 = "delete from memberworkout where workoutid = " + id1 + " and memberusername like '" + this.username + "' ";
                SqlCommand cm2 = new SqlCommand(query2, conn);
                cm2.ExecuteNonQuery();
                cm2.Dispose();

             }
            cm.Dispose();
            string query = "insert into memberworkout(memberusername , workoutId) values ( '" + this.username + "' , " + id.ToString() + ");";
            SqlCommand cmd = new SqlCommand(query, conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            if (result > 0)
            {
                MessageBox.Show("The workout plan has successfully been selected");
                return;
            }
            else
            {
                MessageBox.Show("Error Selecting the workout plan");
            }
        }

        private void tableRefresh()
        {
            DataTable dt = new DataTable();
                        //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();


            string query = "select * from workoutPlan;";
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            string query1 = "select workoutid from memberworkout where memberusername like '" + this.username + "' ";
            SqlCommand cm = new SqlCommand(query1, conn);

            using (SqlDataReader reader = cm.ExecuteReader()) 
            { 

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        int id1 = reader.GetInt32(0);
                        int id = id1 - 1;
                        dataGridView1.Rows[id].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }

            conn.Close();
        }
    }
}
