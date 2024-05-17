using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FlexTrainer
{
    public partial class DietCreate : Form
    {
        string username;
        string[] description = new string[8];
        string[] calories = new string[8];
        string[] allergens = new string[8];
        int index;

        public DietCreate(string username)
        {
            InitializeComponent();
            this.username = username;
            this.index = 0;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dietname = (string)textBox1.Text;
            string goal = comboBox2.Text;
            string type = comboBox1.Text;
            string tcal = label8.Text;
            string author = this.username;

            if (string.IsNullOrEmpty(dietname) || string.IsNullOrEmpty(goal) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please fill out all the details");
                return;
            }
            if (string.IsNullOrEmpty(description[7]))
            {
                MessageBox.Show("Please fill out all the details for all the 7 days.");
                return;
            }
            //Add Connection String here
            SqlConnection conn = new SqlConnection("");
            conn.Open();


            string query = "insert into dietPlan(diet_name, goal, typeDiet, calories, author, authortype) values ('" + dietname + "', '" + goal + "', '" + type + "', " + tcal + ", '" + author + "', 'member'); ";
            SqlCommand cm = new SqlCommand(query, conn);

            cm.ExecuteNonQuery();
            cm.Dispose();




            string query1 = "DECLARE @current INT  = (SELECT SCOPE_IDENTITY()); insert into meal(dietday, diet_Id, mealdescription, kcal, allergens) values (1 , @current, '" + description[1] + "' , " + calories[1] + " , '" + allergens[1] + "'), (2 , @current, '" + description[2] + "' , " + calories[2] + " , '" + allergens[2] + "') , (3 , @current, '" + description[3] + "' , " + calories[3] + " , '" + allergens[3] + "') , (4 , @current, '" + description[4] + "' , " + calories[4] + " , '" + allergens[4] + "') , (5 , @current, '" + description[5] + "' , " + calories[5] + " , '" + allergens[5] + "') , (6 , @current, '" + description[6] + "' , " + calories[6] + " , '" + allergens[6] + "') , (7 , @current, '" + description[7] + "' , " + calories[7] + " , '" + allergens[7] + "');";
            SqlCommand cm1 = new SqlCommand(query1, conn);
            cm1.ExecuteNonQuery();
            cm1.Dispose();
            conn.Close();


            MessageBox.Show("Diet Plan Addition Successfull");
            this.Close();
        }

        private void btnAddDay_Click(object sender, EventArgs e)
        {
            if (this.index == 7)
            {
                MessageBox.Show("All days have already been added!");
                return;
            }
            if (string.IsNullOrEmpty(txtMuscleGroupX.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please fill out all the details for the current day.");
                return;
            }
            this.index++;



            string describe = (string)txtMuscleGroupX.Text;
            string tcal = (string)textBox2.Text;
            string allerg = (string)textBox3.Text;

            this.description[this.index] = describe;
            this.calories[this.index] = tcal;
            this.allergens[this.index] = allerg;

            label6.Text = (this.index + 1).ToString();

            txtMuscleGroupX.Clear();
            textBox2.Clear();
            textBox3.Clear();

            label8.Text = (int.Parse(label8.Text) + int.Parse(tcal)).ToString();

            if (this.index == 7)
            {
                MessageBox.Show("All days added!");
                txtMuscleGroupX.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
