using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jet_Boi_RD.Screens
{
    public partial class DeathScreen : UserControl
    {
        public DeathScreen()
        {
            InitializeComponent();

            youFlewLabel.Text = "You Flew \n 1398M \n And Collected 134 Coins";
        }
    }
}
