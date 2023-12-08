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
    internal class Factory : IRenderableEntity, ICommandable
    {

        public float x = 0, y = 0;
        int width = 32, height = 32;

        public float targetX, targetY;

        public Texture2D texture;
        public Texture2D troopTexture;

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)x, (int)y, width, height);
        }

        public List<(string, int)> getGuiCommands()
        {
            return new List<(string, int)> { 
                ("Make Troop", 15)
            };
        }

        public void receiveCommand(Command command)
        {
            GuiCommand guiCommand = command as GuiCommand;

            if (guiCommand != null && guiCommand.id == "Make Troop")
            {
                Troop troop = new Troop(x, y);
                troop.targetX = targetX;
                troop.targetY = targetY;
                troop.texture = troopTexture;
                guiCommand.entities.Add(troop);
            }
            else
            {
                MoveCommand moveCommand = command as MoveCommand;
                if (moveCommand != null)
                {
                    targetX = moveCommand.x;
                    targetY = moveCommand.y;
                }
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
        }
    }
}
