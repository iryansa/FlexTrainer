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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FlexTrainer.UserControlAdmin
{
    public partial class AdminGymRegisterUC : UserControl
    {
        string username, Name;

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            DialogResult result = MessageBox.Show("Approve the selected gym?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "update gyms set approval = 'Approved' where gymowner = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else if (result == DialogResult.No)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "delete gyms where gymowner = '" + un + "'";
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
            DialogResult result = MessageBox.Show("Approve the selected gym?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "update gyms set approval = 'Approved' where gymowner = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else if (result == DialogResult.No)
            {
                string un = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                string query = "delete gyms where gymowner = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else
            {
                
            }

            conn.Close();
        }

        public AdminGymRegisterUC(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            tablerefresh();
            
        }

        private void tablerefresh()
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            string query = "select * from gyms where approval = 'Approved'";

            DataTable dt = new DataTable();
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

            query = "select * from gyms where approval = 'Unapproved'";

            DataTable dt1 = new DataTable();
            SqlDataAdapter table2 = new SqlDataAdapter(query, conn);
            table2.Fill(dt1);

            dataGridView2.DataSource = dt1;

            conn.Close();
        }
    }
}
