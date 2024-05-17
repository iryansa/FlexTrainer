using FlexTrainer.Properties;
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

namespace FlexTrainer.UserControlsTrainer
{
    public partial class TrainerFeedBackUC : UserControl
    {
        int starvalue = 0;
        string username, Name;
        //Add Connection String here
        SqlConnection conn = new SqlConnection("");

        private void TrainerFeedBackUC_Load(object sender, EventArgs e)
        {
            this.getDataFromDB1();
        }

        public TrainerFeedBackUC(string username, string Name)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(50 , Color.Black);
            this.username = username;
            this.Name = Name;
            label2.Text = Name;
        }

        private void getDataFromDB1()
        {
            conn.Open();
            string query = "select a.apt_id, a.member_username, t.rating, t.description, a.time_slot from appointmentTable a inner join trainerFeedback t on a.apt_id = t.apt_id where a.trainer_username like '" + this.username + "'";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            cm.Dispose();
            //conn.Close();

            //int rating = 0;

            string query2 = "select CAST(AVG(CAST(t.rating AS DECIMAL(10,1))) as decimal (10, 2)) from appointmentTable a inner join trainerFeedback t on a.apt_id = t.apt_id where a.trainer_username like '" + this.username + "' group by a.trainer_username";
            cm = new SqlCommand(query2, this.conn);
            object temp = cm.ExecuteScalar();

            if ((temp is null))
                this.label5.Text = 0.ToString();
            else
                this.label5.Text = temp.ToString();
            cm.Dispose();
            conn.Close();
        }

    }
}
