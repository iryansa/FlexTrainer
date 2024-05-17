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
    public partial class TrainerBookingUC : UserControl
    {
        string username, Name;
        //Add Connection String here
        SqlConnection conn = new SqlConnection("");
        public TrainerBookingUC(string username, string Name)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(50 , Color.Black);
            this.Name = Name;
            this.username = username;
            label2.Text = Name;

            // re-populate the Data Grid View 1
            this.getDataFromDB1();

            // re-populate the Data Grid View 2
            this.getDataFromDB2();

        }


        private void getDataFromDB1()
        {
            this.conn.Open();
            string query = "Select apt_id, time_slot, member_username, Status from appointmentTable where appointmentTable.Status like 'Accepted' and trainer_username = '" + this.username + "' ";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }
        private void getDataFromDB2()
        {
            conn.Open();
            string query = "Select apt_id, time_slot, member_username, Status from appointmentTable where appointmentTable.Status IN ('UnBooked','Pending') and trainer_username = '" + this.username + "' ";
            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView2.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }

        private void refresehBtn_Click(object sender, EventArgs e)
        {
            // re-populate the Data Grid View 1
            this.getDataFromDB1();

            // re-populate the Data Grid View 2
            this.getDataFromDB2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            // first check if the trainer has already created a slot with the same timestamp
            string query1 = "Select * from appointmentTable where appointmentTable.trainer_username like '" + this.username + "' and appointmentTable.time_slot = '" + this.dateTimePicker1.Text + "'";
            SqlCommand cm = new SqlCommand(query1, conn);
            object temp = cm.ExecuteScalar();
            cm.Dispose();
            conn.Close();

            if (temp is null)
            {
                conn.Open();
                string query2 = "insert into appointmentTable (trainer_username, member_username, time_slot, Status) values('" + this.username + "', '" + "-" + "', '" + this.dateTimePicker1.Text + "', 'UnBooked'); ";
                cm = new SqlCommand(query2, conn);
                cm.ExecuteNonQuery();
                cm.Dispose();
                conn.Close();
                // Refresh the corressponding data grid view
                this.getDataFromDB2();
            }
            else
                MessageBox.Show("You have already created a session for the selected slot");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Decline")
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to Decline/Delete this Request", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                    return;

                int id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["apt_idColumn"].Value);

                conn.Open();
                string query = "DELETE from appointmentTable where appointmentTable.apt_id = @id";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@id", id);

                int result = cm.ExecuteNonQuery();
                cm.Dispose();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("The selected Appointment Slot has been Declined Successfully.");
                    this.getDataFromDB2();
                }
                else
                {
                    MessageBox.Show("Unable to Decline the selected Appointment Slot.");
                }


            }
            else if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Accept")
            {
                string check = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells["statusColumn"].Value);

                if (check.Equals("UnBooked"))
                {
                    MessageBox.Show("Cannot Accept an Appointment Request which has yet not been booked by a member.", "Message");
                    return;
                }

                int id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["apt_idColumn"].Value);

                conn.Open();
                string query = "Update appointmentTable set appointmentTable.Status = 'Accepted' where appointmentTable.apt_id = @id";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@id", id);

                int result = cm.ExecuteNonQuery();
                cm.Dispose();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("The selected Appointment Request has been Accepted Successfully.");
                    // Refresh the Data Grid Views
                    this.getDataFromDB1();
                    this.getDataFromDB2();
                }
                else
                {
                    MessageBox.Show("Unable to Accept the selected Appointment Request.");
                }
            }
        }

    }
}
