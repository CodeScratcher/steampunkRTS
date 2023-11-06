using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    public class MoveCommand : Command
    {
        public float x, y;

        public MoveCommand(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
