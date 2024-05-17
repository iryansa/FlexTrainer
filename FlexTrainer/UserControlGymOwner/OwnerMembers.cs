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
    public partial class OwnerMembers : UserControl
    {
        string username, Name;

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();
            DialogResult result = MessageBox.Show("Revoke the membership of the selected member?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                string un = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                string query = "delete membergym where username = '" + un + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                tablerefresh();
            }
            else
            {

            }

            conn.Close();
        }

        private void tablerefresh()
        {
            string query = "Select users.username, Name, email from users inner join membergym on membergym.username = users.username inner join gyms on membergym.gymname = gyms.gymname where gymowner like '" + this.username + "' ";
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();

            

            DataTable dt = new DataTable();
            SqlDataAdapter table1 = new SqlDataAdapter(query, conn);
            table1.Fill(dt);

            dataGridView1.DataSource = dt;

           

            DataTable dt1 = new DataTable();
            SqlDataAdapter table2 = new SqlDataAdapter(query, conn);
            table2.Fill(dt1);

            dataGridView1.DataSource = dt1;

            conn.Close();
        }
        public OwnerMembers(string username, string Name)
        {
            InitializeComponent();
            this.username = username;
            this.Name = Name;
            label2.Text = Name;

            tablerefresh();
        }
    }
}
