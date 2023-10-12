using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal interface IRenderableEntity : IEntity
    {
        void render(SpriteBatch spriteBatch);
    }
}
