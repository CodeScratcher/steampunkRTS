using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal class EnemyTroop : ICommandable, IRenderableEntity
    {
        public float x = 0f, y = 0f;
        int width = 32, height = 32;

        int speed = 4;

        public int hp = 50;

        public Texture2D texture;

        public float targetX = 0f, targetY = 0f;
        public float offsetX = 0f, offsetY = 0f;   
        public EnemyTroop(float x, float y)
        {
            
            this.x = x;
            this.y = y;
            targetX = x;
            targetY = y;
        }
        public Rectangle getBoundingBox()
        {
            //figure out how to give it a position and a width and height
            return new Rectangle((int)x, (int)y, width, height);
        }

        public List<(string, int)> getGuiCommands()
        {
            return new List<(string, int)>();
        }

        public void receiveCommand(Command command)
        {
            MoveCommand moveCommand = command as MoveCommand;

            if (moveCommand != null)
            {
                targetX = moveCommand.x + offsetX;
                targetY = moveCommand.y + offsetY;
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White); 
        }

        public void calculateDamage(List<IEntity> entities)
        {
            foreach (IEntity entity in new List<IEntity>(entities))
            {
                Troop troop = entity as Troop;
                if (troop != null)
                {
                    if (troop.x > x - 100 && troop.x < x + 100 && troop.y > y - 100 && troop.y < y + 100)
                    {
                        Debug.WriteLine($"Enemy Test HP: {hp}");
                        hp -= 1;
                        
                        if (hp <= 0)
                        {
                            entities.Remove(this);
                        }
                    }
                }


            }
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
            if (targetX - x == 0 && targetY - y == 0)
            {
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
    }
}
