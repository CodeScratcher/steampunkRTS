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
        public int x = 0, y = 0;
        int width = 32, height = 32;

        int speed = 5;

        int targetX = 0, targetY = 0;

        public Texture2D texture;

        public Troop(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.targetX = x;
            this.targetY = y;   
        }

        public Rectangle getBoundingBox()
        {
            return new Rectangle(x, y, width, height);
        }

        public void receiveCommand(Command command, int mouseX, int mouseY)
        {
            targetX = mouseX;
            targetY = mouseY;
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        public void update(KeyboardState kstate, MouseState mstate)
        {
            if (x < targetX)
            {
                x += speed;
                if (x > targetX)
                {
                    x = targetX;
                }
            }
            else if (x > targetX)
            {
                x -= speed;
                if (x < targetX)
                {
                    x = targetX;
                }
            }

            if (y < targetY)
            {
                y += speed;
                if (y > targetY)
                {
                    y = targetY;
                }
            }
            else if (y > targetY)
            {
                y -= speed;
                if (y < targetY)
                {
                    y = targetY;
                }
            }
        }
    }
}
