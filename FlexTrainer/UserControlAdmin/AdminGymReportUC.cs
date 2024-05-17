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

namespace FlexTrainer.UserControlAdmin
{

    public partial class AdminGymReportUC : UserControl
    {
        string username, Name;
        //Add Connection String here
        SqlConnection conn = new SqlConnection("");
        private void AdminGymReportUC_Load(object sender, EventArgs e)
        {

            conn.Open();
            string query = "select gymname from gyms where approval = 'Approved'";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            gymname.DisplayMember = "gymname";
            gymname.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query = "select username from trainergym where approved = 'Approved' and gymname = '" + gymname.Text + "'";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            trainername.DisplayMember = "username";
            trainername.DataSource = dt;

            cm.Dispose();

            query = "select username from membergym where gymname = '" + gymname.Text + "'";

            SqlCommand cm1 = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cm1);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);


            membername.DisplayMember = "username";
            membername.DataSource = dt1;

            cm.Dispose();

            conn.Close();
        }

        private void genReportBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select a report type first!");
                return;
            }
            string sel = comboBox2.Text;
            string gym = gymname.Text, trainer = trainername.Text , member = membername.Text;
            string query = "";
            if (sel == "Details of members of one specific gym that get training from 1 specific trainer.")
            {
                query = "select users.username, users.Name, users.email from users inner join membergym on users.username = membergym.username inner join appointmentTable on users.username = appointmentTable.member_username where appointmentTable.trainer_username = '" + trainer + "' and membergym.gymname = '" + gym + "'";
            }
            else if(sel == "Details of members from one specific gym that follow a specific diet plan.")
            {
                query = "SELECT u.username, u.Name, u.email, u.typeuser FROM users u JOIN membergym mg ON u.username = mg.username JOIN memberdiet md ON u.username = md.memberusername JOIN dietPlan dp ON md.dietId = dp.diet_id WHERE mg.gymname = '" + gym + "' AND dp.diet_id = 1;";
            }
            else if(sel == "Details of members across all gyms of a specific trainer that follow a specific diet plan.")
            {
                query = "SELECT u.username, u.Name, u.email, u.typeuser FROM users u JOIN membergym mg ON u.username = mg.username JOIN memberdiet md ON u.username = md.memberusername JOIN dietPlan dp ON md.dietId = dp.diet_id JOIN trainergym tg ON u.username = tg.username WHERE tg.approved = 'Approved' AND tg.gymname IN (SELECT gymname FROM trainergym WHERE username = '" + trainer + "') AND dp.diet_id = 1;";
            }
            else if (sel == "Count of members who will be using specific machines on a given day in a specific gym.")
            {
                query = "SELECT COUNT(DISTINCT username) AS member_count FROM appointmentTable a JOIN membergym mg ON a.member_username = mg.username JOIN excercise e ON a.time_slot = CAST(e.workoutday AS datetime) JOIN gyms g ON mg.gymname = g.gymname WHERE  g.gymname = '" + gym + "' AND e.machine = 'Biceps';";
            }
            else if (sel == "List of Diet plans that have less than 500 calorie meals as breakfast.")
            {
                query = "SELECT DISTINCT dp.* FROM dietPlan dp JOIN meal m ON dp.diet_id = m.diet_Id WHERE m.mealdescription = 'Breakfast' AND m.kcal < 500;";
            }
            else if(sel == "List of diet plans in which total carbohydrate intake is less than 300 grams.")
            {
                query = "select * from dietPlan where calories < 300";
            }
            else if(sel == "List of workout plans that don’t require using a specific machine.")
            {
                query = "select * from excercise where machine not like '%Dumbell%'; ";
            }
            else if(sel == "List of diet plans which doesn’t have peanuts as allergens.")
            {
                query = "SELECT * FROM meal WHERE allergens NOT LIKE '%peanuts%';";
            }
            else if(sel == "New membership data in last 3 months (Gym Owner).")
            {
                query = "select users.username, users.Name, users.email from users inner join membergym on membergym.username = users.username where gymname = '" + gym + "'";
            }
            else if(sel == "Comparison of total members in multiple gyms, in the past 6 months.")
            {
                query = "select users.username, users.Name, users.email, membergym.gymname from users inner join membergym on membergym.username = users.username";
            }
            else if (sel == "List of all users of the application.")
            {
                query = "select users.username, users.Name, users.email , typeuser from users";
            }
            else if(sel == "List of all workoutPlans.")
            {
                query = "select * from workoutPlan";
            }
            else if(sel == "List of all DietPlans.")
            {
                query = "select * from dietPlan";
            }
            else if(sel == "List of all trainers in a specific gym.")
            {
                query = "select * from trainergym where gymname = '" + gym + "' and approved = 'Approved'";
            }
            else if(sel == "List of all Members in a specific gym.")
            {
                query = "select * from membergym where gymname = '" + gym + "'";
            }
            else if(sel == "List of overall ratings of all trainers.")
            {
                query = "SELECT at.trainer_username, CAST(AVG(CAST(tf.rating AS DECIMAL(10,1))) as decimal (10, 2)) AS overall_rating FROM appointmentTable at JOIN trainerFeedback tf ON at.apt_id = tf.apt_id GROUP BY at.trainer_username;";
            }
            else if(sel == "List of all the appointments.")
            {
                query = "select * from appointmentTable";
            }
            else if(sel == "Audit Trail")
            {
                query = "select * from AuditTrail";
            }

            conn.Open();

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }

        public AdminGymReportUC(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;
        }
    }
}
