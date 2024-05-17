using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexTrainer.UserControlGymOwner
{
    
    public partial class OwnerHomeUC : UserControl
    {
        string Name;
        public OwnerHomeUC(string Name)
        {
            InitializeComponent();
            this.Name = Name;
            label2.Text = Name;

        }
    }
}
