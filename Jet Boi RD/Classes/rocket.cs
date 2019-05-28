using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    public class rocket
    {
        public int x, y;
        public Rectangle hb;
        public bool launchingRocket = true;
        public int launchingTicks = 0;

        public rocket(int _x, int _y)
        {
            x = _x;
            y = _y;
            hb = new Rectangle(x, y, 40, 25);
        }
        public void move()
        {
            x -= 40;
            hb.X -= 40;
        }
    }
}
