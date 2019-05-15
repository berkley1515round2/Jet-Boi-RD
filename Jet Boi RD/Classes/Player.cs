using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    class Player
    {
        public int x, y;
        public Rectangle hb;
        public const int height = 50;
        public const int width = 20;
        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
            hb = new Rectangle(x, y, width, height);

        }
    }
}
