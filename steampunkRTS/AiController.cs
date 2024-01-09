using Microsoft.Xna.Framework;
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
    internal class AiController : Controller, IEntity
    {
        int money = 100;

        public List<IEntity> entities;

        TextureMap textureMap;

        float averageX = 0;
        float averageY = 0;

        int numberOfMines = 0;
        int numberOfFactories = 0;
        int numberOfTroops = 0;

        float timer;

        Random random = new Random();
        public AiController(List<IEntity> entities, TextureMap map)
        {
            this.entities = entities;
            textureMap = map;
            random = new Random();
        }

        private bool acceptablyClose(float x, float y, float targetX, float targetY)
        {
            return (x > targetX - 101 && x < targetX + 101 && y > targetY - 101 && y < targetY + 101);
        }

        public void commandEntities(KeyboardState keyboardState, MouseState mouseState)
        {
            float newAverageX = 0;
            float newAverageY = 0;

            int numberOfEnemies = 0;

            float newTimer = timer;

            for (int i = 0; i < entities.Count(); i++)
            {
                IEntity entity = entities[i];
                EnemyTroop troop = entity as EnemyTroop;
                if (troop != null)
                {

                    if ((averageX != 0 || averageY != 0) && !acceptablyClose(troop.targetX, troop.targetY, averageX, averageY) && timer > 1)
                    {
                        troop.receiveCommand(new MoveCommand(averageX + random.Next(-100, 100), averageY + random.Next(-100, 100)));
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
                        if (enemyFactory != null && money > 15 && numberOfTroops < (numberOfFactories + numberOfMines) * 3 && timer > 1)
                        {
                            money -= 15;
                            enemyFactory.targetX = averageX == 0 ? enemyFactory.x : averageX;
                            enemyFactory.targetY = averageY == 0 ? enemyFactory.y : averageY;

                            enemyFactory.receiveCommand(new GuiCommand("Make Troop", entities));
                            numberOfTroops++;
                        }
                    }
                }
            }

            if (numberOfEnemies != 0)
            {
                averageX = newAverageX / numberOfEnemies;
                averageY = newAverageY / numberOfEnemies;
            }

            

            if (money > 75 && timer > 1)
            {
                money -= 75;
                numberOfMines++;

                Mine mine = new Mine();
                mine.x = 300 + random.Next(-100, 100);
                mine.y = 300 + random.Next(-100, 100);
                mine.texture = textureMap.Get(TextureID.MINE);
                mine.controller = this;
                entities.Add(mine);

                newTimer = 0;
            }

            if (money > 15 && numberOfFactories < numberOfMines * 2 && timer > 1)
            {
                money -= 15;
                numberOfFactories++;

                EnemyFactory factory = new EnemyFactory();
                factory.random = random;
                factory.x = 250 + random.Next(-100, 100);
                factory.y = 250 + random.Next(-100, 100);
                factory.texture = textureMap.Get(TextureID.FACTORY);
                factory.troopTexture = textureMap.Get(TextureID.TROOP);
                entities.Add(factory);

                newTimer = 0;
            }

            timer = newTimer;
            
        }

        public int getMoney()
        {
            return money;
        }

        public void setMoney(int x)
        {
            money = x;
        }

        public void update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
