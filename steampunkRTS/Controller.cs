﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal interface Controller
    {
        void commandEntities(KeyboardState keyboardState, MouseState mouseState);
    }
}
