using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal interface ICommandable
    {
        List<String> getGuiCommands();
        Rectangle getBoundingBox();
        void receiveCommand(Command command);
    }
}
