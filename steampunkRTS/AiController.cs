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

        float averageX = 0;
        float averageY = 0;
        public void commandEntities(KeyboardState keyboardState, MouseState mouseState)
        {
            float newAverageX = 0;
            float newAverageY = 0;

            int numberOfEnemies = 0;
            foreach (IEntity entity in entities)
            {
                EnemyTroop troop = entity as EnemyTroop;
                if (troop != null)
                {
                    if (averageX != 0 && averageY != 0)
                    {
                        troop.receiveCommand(new MoveCommand(averageX, averageY));
                    }
                }
                else
                {
                    Troop playerTroop = entity as Troop;
                    if (playerTroop != null)
                    {
                        newAverageX += playerTroop.x;
                        newAverageY += playerTroop.y;
                    }
                }
            }

            if (numberOfEnemies != 0)
            {
                averageX = newAverageX / numberOfEnemies;
                averageY = newAverageY / numberOfEnemies;
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
