using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    public enum TextureID
    {
        FACTORY,
        MINE,
        TROOP
    }
    internal class TextureMap
    {
        private Dictionary<TextureID, Texture2D> map;

        public TextureMap()
        {
            map = new Dictionary<TextureID, Texture2D>();
        }

        public void Add(TextureID id, Texture2D texture)
        {
            map.Add(id, texture);
        }

        public Texture2D Get(TextureID id)
        {
            return map[id];
        }
    }
}
