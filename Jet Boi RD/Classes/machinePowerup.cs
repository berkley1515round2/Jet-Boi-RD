using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jet_Boi_RD.Classes
{
    class machinePowerup
    {
        const int teleporter = 0;
        const int superJump = 1;
        const int gravity = 2;
        public machinePowerup(string type)
        {
            
        }

        public machinePowerup(int code)
        {
            switch(code)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
