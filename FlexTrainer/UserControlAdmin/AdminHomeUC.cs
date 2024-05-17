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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FlexTrainer.UserControlAdmin
{
    public partial class AdminHomeUC : UserControl
    {
        string Name;
        public AdminHomeUC(string Name)
        {
            InitializeComponent();
            this.Name = Name;
            label2.Text = Name;
            
        }

    }
}
