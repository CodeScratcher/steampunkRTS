﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace steampunkRTS
{
    internal interface Entity
    {
        void update(KeyboardState kstate, MouseState mstate);
    }
}
