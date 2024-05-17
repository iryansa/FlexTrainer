using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer.UserControlGymOwner
{
    public partial class OwnerNewGymUC : UserControl
    {
        string username, Name;
        bool gymflag = false;

        private void button2_Click(object sender, EventArgs e)
        {
            string gymname = textBox1.Text;
            string business = textBox2.Text;
            string facility = textBox3.Text;
            string currentmembers = textBox4.Text;

            gymname.Replace("'", "\'");
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query = "insert into gyms(gymowner, gymname, businessplan, facility, currentmembers, approval) values ('" + this.username + "' , '" + gymname + "' , ' " + business + "' , '" + facility + "' , " + currentmembers + " , 'Unapproved')";
            SqlCommand cmd = new SqlCommand(query, conn);
            int result = cmd.ExecuteNonQuery();

            if(result > 0)
            {
                MessageBox.Show("Successful!");
            }
            else
            {
                MessageBox.Show("Error!");
            }

            cmd.Dispose();
            conn.Close();

            tablerefresh();
        }

        public OwnerNewGymUC(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query = "select * from gyms where gymowner like '" + this.username + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            object result = cmd.ExecuteScalar();

            if(result != null)
            {
                gymflag = true;
            }

            cmd.Dispose();

            if(gymflag )
            {

                tablerefresh();

            }

            conn.Close();


        }

        private void tablerefresh()
        {

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            button2.Enabled = false;
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query = "select * from gyms where gymowner like '" + this.username + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
