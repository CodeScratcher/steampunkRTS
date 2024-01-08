using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public AiController(List<IEntity> entities, TextureMap map)
        {
            this.entities = entities;

            Mine mine = new Mine();
            mine.controller = this;
            mine.x = 150;
            mine.y = 150;
            mine.texture = map.Get(TextureID.MINE);

            entities.Add(mine);

            EnemyFactory enemyFactory = new EnemyFactory();
            enemyFactory.x = 100;
            enemyFactory.y = 100;
            enemyFactory.texture = map.Get(TextureID.FACTORY);
            enemyFactory.troopTexture = map.Get(TextureID.TROOP);

            entities.Add(enemyFactory);
        }
        public void commandEntities(KeyboardState keyboardState, MouseState mouseState)
        {
            float newAverageX = 0;
            float newAverageY = 0;

            int numberOfEnemies = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                IEntity entity = entities[i];
                EnemyTroop troop = entity as EnemyTroop;
                if (troop != null)
                {
                    
                    if (averageX != 0 || averageY != 0)
                    {
                        troop.receiveCommand(new MoveCommand(averageX, averageY));
                    }
                }
                else
                {
                    Troop playerTroop = entity as Troop;
                    if (playerTroop != null)
                    {
                        numberOfEnemies++;
                        newAverageX += playerTroop.x;
                        newAverageY += playerTroop.y;
                    }
                    else
                    {
                        EnemyFactory enemyFactory = entity as EnemyFactory;
                        if (enemyFactory != null && money > 15)
                        {
                            money -= 15;
                            enemyFactory.targetX = averageX == 0 ? enemyFactory.x : averageX;
                            enemyFactory.targetY = averageY == 0 ? enemyFactory.y : averageY;

                            enemyFactory.receiveCommand(new GuiCommand("Make Troop", entities));
                        }
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
            return money;
        }

        public void setMoney(int x)
        {
            money = x;
        }
    }
}
