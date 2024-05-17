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

namespace FlexTrainer.UserControlGymOwner
{
    public partial class OwnerTrainerApprove : UserControl
    {
        string username, Name;
        public OwnerTrainerApprove(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            tablerefresh();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            DialogResult result = MessageBox.Show("Approve the selected trainer?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "update trainergym set approval = 'Approved' where username = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else if (result == DialogResult.No)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "delete trainergym where username = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else
            {

            }

            conn.Close();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            DialogResult result = MessageBox.Show("Approve the selected trainer?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "update trainergym set approved = 'Approved' where username = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else if (result == DialogResult.No)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "delete trainergym where username = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else
            {

            }

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sign Out and Register again for a new trainer!");
            return;
        }

        private void tablerefresh()
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query = "select users.username, users.Name, expertise, qualification, trainergym.approved from users inner join trainergym on users.username = trainergym.username inner join gyms on trainergym.gymname = gyms.gymname where trainergym.approved = 'Approved' and gyms.gymowner like '" + this.username + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            query = "select users.username, users.Name, expertise, qualification, trainergym.approved from users inner join trainergym on users.username = trainergym.username inner join gyms on trainergym.gymname = gyms.gymname where trainergym.approved = 'Unapproved' and gyms.gymowner like '" + this.username + "' "; ;

            DataTable dt1 = new DataTable();
            SqlDataAdapter table2 = new SqlDataAdapter(query, conn);
            table2.Fill(dt1);

            dataGridView2.DataSource = dt1;

            conn.Close();
        }
    }
}
