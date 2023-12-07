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
    internal class EnemyTroop : ICommandable, IRenderableEntity,
    {
        public Rectangle getBoundingBox()
        {
            throw new NotImplementedException();
        }

        public List<(string, int)> getGuiCommands()
        {
            throw new NotImplementedException();
        }

        public void receiveCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void render(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
