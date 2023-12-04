using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal interface Controller
    {

        void setMoney(int x);
        int getMoney();
        void commandEntities(KeyboardState keyboardState, MouseState mouseState);
    }
}
