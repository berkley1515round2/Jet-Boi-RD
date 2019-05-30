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
            teleporterButton.Enabled = !GameScreen.mechs["teleporter"];
            gravitySuitButton.Enabled = !GameScreen.mechs["gravity"];
            hogButton.Enabled = !GameScreen.mechs["superJump"];




            

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form1.switchScreen(this, "game");
        }

        
    }
}
