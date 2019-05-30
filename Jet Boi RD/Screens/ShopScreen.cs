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
    public partial class ShopScreen : UserControl
    {
        public static bool switchS = false;



        public ShopScreen()
        {
            InitializeComponent();

            if (switchS)
            {
                Form1.switchScreen(this, "game");
                switchS = false;
            }

        }

        
    }
}
