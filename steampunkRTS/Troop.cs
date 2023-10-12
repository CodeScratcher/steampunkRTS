using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace steampunkRTS
{
    internal class Troop : IRenderableEntity, ICommandable
    {
        int x = 0, y = 0;
        int width = 32, height = 32;

        public Texture2D texture;

        public Rectangle getBoundingBox()
        {
            return new Rectangle(x, y, width, height);
        }

        public void receiveCommand(Command command, int mouseX, int mouseY)
        {
            x = mouseX;
            y = mouseY;
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        public void update(KeyboardState kstate, MouseState mstate)
        {

        }
    }
}
