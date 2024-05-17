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
    public partial class MemberBookTrainerUC : UserControl
    {
        string username, Name;
        //Add Connection String here
        SqlConnection conn = new SqlConnection("");
        public MemberBookTrainerUC(string username, string Name)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(50 , Color.Black);
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            // populate the DataGridViews with the Relevant Data
            this.getDataFromDB1();
            this.getDataFromDB2();

        }


        private void getDataFromDB1()
        {
            conn.Open();
            string query = "Select apt_id, trainer_username, time_slot from appointmentTable inner join trainergym on trainer_username = trainergym.username inner join membergym on membergym.gymname = trainergym.gymname where appointmentTable.Status like 'UnBooked' and membergym.username = '" + this.username + "' ";

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
            string query = "Select apt_id, trainer_username, time_slot, status from appointmentTable where appointmentTable.member_username like '" + this.username + "' AND appointmentTable.Status in ('Pending','Accepted')";

            SqlCommand cm = new SqlCommand(query, this.conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView2.DataSource = dt;

            cm.Dispose();
            conn.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Book")
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["apt_idColumn"].Value);

                conn.Open();
                string query = "Update appointmentTable set appointmentTable.Status = 'Pending', appointmentTable.member_username = '" + this.username + "' where appointmentTable.apt_id = @id";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@id", id);

                int result = cm.ExecuteNonQuery();
                cm.Dispose();
                conn.Close();

                if (result > 0)
                {
                    MessageBox.Show("The selected Slot has been Booked Successfully.");
                    // Refresh the Data Grid Views
                    this.getDataFromDB1();
                    this.getDataFromDB2();
                }
                else
                {
                    MessageBox.Show("Unable to Book the selected Slot.");
                }
            }
        }
        private void refresehBtn_Click(object sender, EventArgs e)
        {
            // re-populate the Data Grid View 1
            this.getDataFromDB1();

            // re-populate the Data Grid View 2
            this.getDataFromDB2();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
