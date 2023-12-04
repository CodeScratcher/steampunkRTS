using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public float x = 0f, y = 0f;
        int width = 32, height = 32;

        int speed = 5;

        public float targetX = 0f, targetY = 0f;

        public Texture2D texture;

        public Troop(float x, float y)
        {
            this.x = x;
            this.y = y;
            targetX = x;
            targetY = y;   
        }

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)x, (int)y, width, height);
        }

        public void receiveCommand(Command command)
        {
            MoveCommand moveCommand = command as MoveCommand;

            if (moveCommand != null)
            {
                targetX = moveCommand.x;
                targetY = moveCommand.y;
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
            if (targetX - x == 0 && targetY - y == 0) {
                return;
            }
            Vector2 movement = Vector2.Normalize(new Vector2(targetX - x, targetY - y)) * speed;

            bool right = x < targetX;
            bool down = y < targetY;
            
            x += movement.X;
            y += movement.Y;

            bool rightNow = x < targetX;
            bool downNow = y < targetY;

            if ((right && !rightNow) || (!right && rightNow))
            {
                x = targetX;
            }

            if ((down && !downNow) || (!down && downNow))
            {
                y = targetY;
            }
            
        }

        public List<(string, int)> getGuiCommands()
        {
            return new List<(string, int)>();
        }
    }
}
