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

        public float x = 0, y = 0;
        int width = 32, height = 32;

        float targetX, targetY;

        public Texture2D texture;
        public Texture2D troopTexture;

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)x, (int)y, width, height);
        }

        public List<string> getGuiCommands()
        {
            return new List<string> { 
                "Make Troop"
            };
        }

        public void receiveCommand(Command command)
        {
            switch (command)
            {
                case Command.MOVE:
                    targetX = x;
                    targetY = y;
                    break;
                case Command.GUI_COMMAND:
                    Troop troop = new Troop(x, y);
                    troop.targetX = targetX;
                    troop.targetY = targetY;
                    troop.texture = troopTexture;
                    entities.Add(troop);
                    break;
            }
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
