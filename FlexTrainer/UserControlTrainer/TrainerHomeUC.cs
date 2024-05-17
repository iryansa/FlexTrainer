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
    public partial class TrainerHomeUC : UserControl
    {
        string Name;
        string username;
        public TrainerHomeUC(string name, string username)
        {
            InitializeComponent();
            Name = name;
            label2.Text = name;
            this.username = username;
        }

        private void TrainerHomeUC_Load(object sender, EventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query1 = "select gymname from trainergym where username = '" + this.username + "'";
            SqlCommand cmd = new SqlCommand(query1, conn);
            string gymname = (string)cmd.ExecuteScalar();
            if (!string.IsNullOrEmpty(gymname))
            {
                label4.Text = "Your Gym: " + gymname;
                comboBox1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                string query = "select gymname from gyms where approval like 'Approved'";

                SqlCommand cm = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox1.DisplayMember = "gymname";
                comboBox1.DataSource = dt;

                cm.Dispose();

            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selected = comboBox1.Text;
            string query = "insert into trainergym(username, gymname, approved) values ('" + this.username + "' , '" + selected + "' , 'Unapproved')";
                        //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();

            MessageBox.Show("Successful!");
        }

        
    }
}
