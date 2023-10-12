using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    enum Command { 
        MOVE
    }
    internal interface ICommandable
    {
        Rectangle getBoundingBox();
        void receiveCommand(Command command, int x, int y);
    }
}
