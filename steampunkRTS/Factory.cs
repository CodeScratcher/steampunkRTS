using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal class Factory : IRenderableEntity, ICommandable
    {

        int x = 0, y = 0, width = 0, height = 0;

        int targetX, targetY;

        Texture texture;
        public Rectangle getBoundingBox()
        {
            return new Rectangle(x, y, width, height);
        }

        public List<string> getGuiCommands()
        {
            return new List<string> { 
                "Make Troop"
            };
        }

        public void receiveCommand(Command command, string guiType, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void render(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void update(KeyboardState kstate, MouseState mstate)
        {
            throw new NotImplementedException();
        }
    }
}
