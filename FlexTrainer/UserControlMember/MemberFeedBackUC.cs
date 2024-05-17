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

namespace FlexTrainer.UserControlsMember
{
    public partial class MemberFeedBackUC : UserControl
    {
        int starvalue = 0;
        string username, Name;
        //Add Connection String here
        SqlConnection conn = new SqlConnection("");
        public MemberFeedBackUC(string username, string Name)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(50 , Color.Black);
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            this.getDataFromDB1();
        }
        private void getDataFromDB1()
        {
            conn.Open();
            string query = "select a.apt_id, a.trainer_username, t.rating, t.description, a.time_slot from appointmentTable a inner join trainerFeedback t on a.apt_id = t.apt_id where a.member_username like '" + this.username + "'";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            cm.Dispose();
            conn.Close();

        }

        private void MemberFeedBackUC_Load(object sender, EventArgs e)
        {
            this.loadTheComboBox();
        }
        private void loadTheComboBox()
        {
            //this.comboBox2.Items.Clear();

            conn.Open();
            string query = "select apt_id, CONCAT(apt_id, ', ', trainer_username) as 'apt_id_trainer_username' from appointmentTable a where NOT EXISTS(select apt_id from trainerFeedback as t where t.apt_id = a.apt_id) and a.time_slot < GETDATE() and a.member_username like '" + this.username + "' and a.Status like 'Accepted'";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            /*            foreach (DataRow dr in dt.Rows)
                        {
                            comboBox2.Items.Add(dr[0].ToString()); 
                        }*/
            comboBox2.DisplayMember = "apt_id_trainer_username";
            comboBox2.ValueMember = "apt_id";
            comboBox2.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.star__1_;
            starvalue = 1;
            pictureBox2.Image = Resources.star;
            pictureBox3.Image = Resources.star;
            pictureBox4.Image = Resources.star;
            pictureBox5.Image = Resources.star;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.star__1_;
            starvalue = 2;
            pictureBox2.Image = Resources.star__1_;
            pictureBox3.Image = Resources.star;
            pictureBox4.Image = Resources.star;
            pictureBox5.Image = Resources.star;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.star__1_;
            starvalue = 3;
            pictureBox2.Image = Resources.star__1_;
            pictureBox3.Image = Resources.star__1_;
            pictureBox4.Image = Resources.star;
            pictureBox5.Image = Resources.star;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.star__1_;
            starvalue = 4;
            pictureBox2.Image = Resources.star__1_;
            pictureBox3.Image = Resources.star__1_;
            pictureBox4.Image = Resources.star__1_;
            pictureBox5.Image = Resources.star;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.star__1_;
            starvalue = 5;
            pictureBox2.Image = Resources.star__1_;
            pictureBox3.Image = Resources.star__1_;
            pictureBox4.Image = Resources.star__1_;
            pictureBox5.Image = Resources.star__1_;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.resetRating();
        }

        private void resetRating()
        {
            starvalue = 0;

            pictureBox1.Image = Resources.star;
            pictureBox2.Image = Resources.star;
            pictureBox3.Image = Resources.star;
            pictureBox4.Image = Resources.star;
            pictureBox5.Image = Resources.star;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(comboBox2.SelectedValue);
            if (id == 0)
            {
                MessageBox.Show("Please Select a Trainer to Review", "Message");
                return;
            }

            if (this.starvalue == 0)
            {
                MessageBox.Show("You have not selected a Rating for the Feedback", "Message");
                return;
            }

            conn.Open();
            string query = "insert into trainerFeedback VALUES (@id, @rating, '" + textBox1.Text.ToString() + "')";
            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@id", id);
            cm.Parameters.AddWithValue("@rating", this.starvalue);

            int result = cm.ExecuteNonQuery();
            this.textBox1.Clear();
            cm.Dispose();
            conn.Close();

            if (result > 0)
            {
                MessageBox.Show("Your Feedback has been recorded", "Message");
                this.loadTheComboBox();
                this.getDataFromDB1();
            }
            else
            {
                MessageBox.Show("Unable to submit Feedback for the selected Trainer", "Message");
            }
            //MessageBox.Show(value);


            this.resetRating();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox6, "Clear Rating");
        }
    }
}
