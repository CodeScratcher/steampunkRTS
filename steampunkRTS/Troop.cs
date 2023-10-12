using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace steampunkRTS
{
    internal class Troop : IRenderableEntity, ICommandable
    {
        int x = 0, y = 0;
        public void receiveCommand(Command command, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void render()
        {
            throw new NotImplementedException();
        }

        public void update(KeyboardState kstate, MouseState mstate)
        {

        }
    }
}
