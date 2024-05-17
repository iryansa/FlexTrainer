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
    public partial class Register : Form
    {
        Form1 prev;
        public Register(Form1 prev)
        {
            InitializeComponent();
            groupBox1.BackColor = Color.FromArgb(175, Color.Black);
            groupBox2.BackColor = Color.FromArgb(50, Color.Black);
            this.prev = prev;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prev.Close();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemberRegister member = new MemberRegister(this, prev);
            this.Hide();
            member.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainerRegister trainer = new TrainerRegister(this, prev);
            this.Hide();
            trainer.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OwnerRegister trainer = new OwnerRegister(this, prev);
            this.Hide();
            trainer.ShowDialog();
        }
    }
}
