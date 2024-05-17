using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox1.BackColor = Color.FromArgb(175, Color.Black);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 login = new Form2(this);
            this.Hide();
            login.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register form = new Register(this);
            this.Hide();
            form.ShowDialog();
        }
    }
}
