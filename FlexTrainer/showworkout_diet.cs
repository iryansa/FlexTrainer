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

namespace FlexTrainer
{
    public partial class showworkout_diet : Form
    {
        int id;
        string username;
        string labeltext;
        public showworkout_diet(int id, string username, string labeltext)
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
            this.labeltext = labeltext;
        }

        private void showworkout_diet_Load(object sender, EventArgs e)
        {
            this.Text = this.labeltext;
            label1.Text = this.labeltext;
            tableRefresh();

        }

        private void tableRefresh()
        {
            DataTable dt = new DataTable();
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            string query = "";
            if (this.labeltext == "FlexTrainer - Diet Plan Info")
            {
                query = "select * from meal where diet_id = " + this.id.ToString();
            }
            else if(this.labeltext == "FlexTrainer - Diet Plan Report")
            {
                query = "select dietplan.diet_id, dietplan.diet_name, memberusername as 'Follower', author, goal, calories, typeDiet as 'Type' from dietplan inner join memberdiet on diet_id = dietId inner join users on memberusername = username where diet_id = " + id.ToString();
            }
            else if(this.labeltext == "FlexTrainer - Workout Plan Report")
            {
                query = "select workoutplan.workout_id, workoutplan.workout_name, memberusername as 'Follower', author, goal, exp_level as 'Level', schedule from workoutplan inner join memberworkout on workout_id = workoutId inner join users on memberusername = username where workout_id = " + id.ToString();
            }
            else if(this.labeltext == "FlexTrainer - Trainer Report")
            {
                query = "select a.apt_id, a.member_username, t.rating, t.description, a.time_slot from appointmentTable a inner join trainerFeedback t on a.apt_id = t.apt_id where a.trainer_username like '" + this.username + "'";
                string query2 = "select CAST(AVG(CAST(t.rating AS DECIMAL(10,1))) as decimal (10, 2)) from appointmentTable a inner join trainerFeedback t on a.apt_id = t.apt_id where a.trainer_username like '" + this.username + "' group by a.trainer_username";
                SqlCommand cm = new SqlCommand(query2, conn);
                object temp = cm.ExecuteScalar();
                string rating;
                if ((temp is null))
                    rating = 0.ToString();
                else
                    rating = temp.ToString();

                cm.Dispose();

                labelextra.Text = "Trainer Username: " + username + " , Overall Rating: " + rating;
            }
            else if(this.labeltext == "FlexTrainer - Member Report")
            {
                string query2 = "select gymname from membergym where username = '" + username + "'";
                SqlCommand cm = new SqlCommand(query2, conn);
                object temp = cm.ExecuteScalar();
                string rating;
                if ((temp is null))
                    rating = 0.ToString();
                else
                    rating = temp.ToString();

                cm.Dispose();

                labelextra.Text = "Member Username: " + username + " , Gym: " + rating;

                query = "select username, Name, email, workoutId, dietId from users inner join memberworkout on username = memberworkout.memberusername inner join memberdiet on username = memberdiet.memberusername where username = '" + username + "'";
            }
            else
            {
                query = "select * from excercise where workoutid = " + this.id.ToString();
            }
           
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
