using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace steampunkRTS
{
    internal class Troop : IRenderableEntity, ICommandable
    {
        int x = 0, y = 0;
        int width = 32, height = 32;

        public Rectangle getBoundingBox()
        {
            return new Rectangle(x, y, width, height);
        }

        public void receiveCommand(Command command, int mouseX, int mouseY)
        {
            x = mouseX;
            y = mouseY;
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
