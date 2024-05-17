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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FlexTrainer.UserControlGymOwner
{
    public partial class OwnerReportUC : UserControl
    {
        string username, Name;

        private void OwnerReportUC_Load(object sender, EventArgs e)
        {

            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();


            string query = "select username from trainergym inner join gyms on trainergym.gymname = gyms.gymname where trainergym.approved = 'Approved' and gymowner = '" + this.username + "'";

            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = dt;




            cm.Dispose();

            query = "select username from membergym inner join gyms on membergym.gymname = gyms.gymname where gymowner = '" + this.username + "'";

            SqlCommand cm1 = new SqlCommand(query, conn);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cm1);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);

            comboBox4.DisplayMember = "username";
            comboBox4.DataSource = dt1;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string us = comboBox1.Text;
            showworkout_diet form1 = new showworkout_diet(0, us, "FlexTrainer - Trainer Report");
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string us = comboBox4.Text;
            showworkout_diet form1 = new showworkout_diet(0, us, "FlexTrainer - Member Report");
            form1.ShowDialog();
        }

        public OwnerReportUC(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;
        }
    }
}
