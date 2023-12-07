using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal class AiController : Controller
    {
        int money = 100;

        public List<IEntity> entities;
        public void commandEntities(KeyboardState keyboardState, MouseState mouseState)
        {
            foreach (IEntity entity in entities)
            {
                EnemyTroop troop = entity as EnemyTroop;
                if (troop != null)
                {
                    troop.receiveCommand(new MoveCommand(0, 0));
                }
            }
        }

        public int getMoney()
        {
            throw new NotImplementedException();
        }

        public void setMoney(int x)
        {
            throw new NotImplementedException();
        }
    }
}
