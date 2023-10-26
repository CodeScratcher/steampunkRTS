using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal class PlayerController : Controller
    {
        ICommandable selectedEntity = null;

        public void commandEntities(KeyboardState kstate, MouseState mstate, List<IEntity> entities)
        {
            if (mstate.LeftButton == ButtonState.Pressed)
            {
                bool shouldDeselect = true;
                foreach (IEntity entity in entities)
                {
                    ICommandable commandable = entity as ICommandable;

                    if (commandable != null)
                    {
                        if (commandable.getBoundingBox().Contains(new Vector2(mstate.X, mstate.Y)))
                        {
                            selectedEntity = commandable;
                            shouldDeselect = false;
                        }
                    }   
                }

                if (shouldDeselect)
                {
                    selectedEntity = null;
                }
            }

            if (mstate.RightButton == ButtonState.Pressed && selectedEntity != null)
            {
                selectedEntity.receiveCommand(Command.MOVE, mstate.X, mstate.Y);
            }
        }
    }
}
