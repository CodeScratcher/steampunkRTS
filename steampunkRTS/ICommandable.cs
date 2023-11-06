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
        MOVE,
        GUI_COMMAND
    }
    internal interface ICommandable
    {
        List<String> getGuiCommands();
        Rectangle getBoundingBox();
        void receiveCommand(Command command, string guiType, int x, int y, List<IEntity> entities);
    }
}
