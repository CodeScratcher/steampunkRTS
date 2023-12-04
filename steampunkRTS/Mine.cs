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
    internal class Mine : IRenderableEntity
    {
        public float x = 0, y = 0;
        public Texture2D texture;
        public Controller controller;
        public float buildup;
       

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
            buildup += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (buildup > 1)
            {
                buildup -= 1;

                controller.setMoney(controller.getMoney() + 10);
            }
        }
    }
}
