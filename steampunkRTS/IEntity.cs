using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace steampunkRTS
{
    public interface IEntity
    {
        void update(KeyboardState kstate, MouseState mstate, GameTime gameTime);
    }
}
