using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Attributes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    internal class PlayerController : Controller
    {
        private enum ControllerState
        {
            NORMAL,
            PLACING_FACTORY,
            PLACING_MINE
        }

        ICommandable selectedEntity;

        public List<TextButton> buttons;
        public List<TextButton> globalButtons;

        public List<IEntity> entities;

        public Grid grid;

        public int money = 100;

        public Label label;

        private ControllerState mode;

        private TextureMap map;

        private Camera camera;

        public PlayerController(Grid grid, Camera camera, List<IEntity> entities, TextureMap map)
        {
            this.camera = camera;
            buttons = new List<TextButton>();
            globalButtons = new List<TextButton>();
            this.grid = grid;
            this.entities = entities;

            label = new Label();

            Grid.SetColumn(label, 8);
            label.Text = money.ToString();

            grid.Widgets.Add(label);

            TextButton button = new TextButton
            {
                Text = "Place Factory"
            };

            button.Click += (s, a) =>
            {
                if (money >= 15)
                {
                    mode = ControllerState.PLACING_FACTORY;
                }
                
            };

            globalButtons.Add(button);

            Grid.SetColumn(button, 6);
            grid.Widgets.Add(button);

            TextButton mineButton = new TextButton
            {
                Text = "Place Mine"
            };

            mineButton.Click += (s, a) =>
            {
                if (money >= 75)
                {
                    mode = ControllerState.PLACING_MINE;
                }
            };

            globalButtons.Add(mineButton);

            Grid.SetColumn(mineButton, 6);
            Grid.SetRow(mineButton, 1);
            grid.Widgets.Add(mineButton);

            this.map = map;
        }

        private void removeButtons() { 
            foreach (TextButton button in buttons)
            {
                grid.Widgets.Remove(button);
            }
            buttons.Clear(); 
        }

        private void generateButtons(ICommandable entity)
        {

            removeButtons();

            int i = 1;
            foreach ((string str, int cost) in entity.getGuiCommands())
            {
                TextButton button = new TextButton
                {
                    Text = str
                };

                button.Click += (s, a) =>
                {
                    if (money >= cost)
                    {
                        entity.receiveCommand(new GuiCommand(str, entities));
                        money -= cost;
                    }
                    

                    label.Text = money.ToString();
                };

                Grid.SetColumn(button, 8);
                Grid.SetRow(button, i);

                grid.Widgets.Add(button);

                buttons.Add(button);

                i++;
            }

            
        }

        public void commandEntities(KeyboardState kstate, MouseState mstate)
        {
            Vector2 mousePosition;
            Vector2 screenPosition = new Vector2(mstate.X, mstate.Y);
            
            camera.ToWorld(ref screenPosition, out mousePosition);

            mousePosition.X -= camera.Width / 2;
            mousePosition.Y -= camera.Height / 2;
          
            if (mstate.LeftButton == ButtonState.Pressed)
            {
                if (mode == ControllerState.NORMAL) { 
                    bool shouldDeselect = true;

                    foreach (IEntity entity in entities)
                    {
                        ICommandable commandable = entity as ICommandable;

                        if (commandable != null && entity as EnemyTroop == null)
                        {
                            if (commandable.getBoundingBox().Contains(new Vector2((int)mousePosition.X, (int)mousePosition.Y)))
                            {
                                selectedEntity = commandable;
                                generateButtons(selectedEntity);
                                shouldDeselect = false;
                            }
                        }   
                    }

                    foreach (TextButton button in buttons)
                    {
                        if (button.ContainsGlobalPoint(new Point((int)mstate.X, (int)mstate.Y)))
                        {
                            shouldDeselect = false;
                        }
                    }
                    foreach (TextButton button in globalButtons)
                    {
                        if (button.ContainsGlobalPoint(new Point((int)mstate.X, (int)mstate.Y)))
                        {
                            shouldDeselect = false;
                        }
                    }

                    if (shouldDeselect)
                    {
                        removeButtons();
                        selectedEntity = null;
                    }
                }
                else if (mode == ControllerState.PLACING_FACTORY)
                {
                    Factory newFactory = new Factory();
                    newFactory.x = (int)mousePosition.X;
                    newFactory.targetX = (int)mousePosition.X;

                    newFactory.y = (int)mousePosition.Y;
                    newFactory.targetY = (int)mousePosition.Y;

                    newFactory.texture = map.Get(TextureID.FACTORY);
                    newFactory.troopTexture = map.Get(TextureID.TROOP);

                    entities.Add(newFactory);

                    money -= 15;
                    label.Text = money.ToString();
                    mode = ControllerState.NORMAL;
                }
                else if (mode == ControllerState.PLACING_MINE)
                {
                    Mine newMine = new Mine();

                    newMine.x = (int)mousePosition.X;
                    newMine.y = (int)mousePosition.Y;

                    newMine.texture = map.Get(TextureID.MINE);

                    newMine.controller = this;

                    entities.Add(newMine);

                    money -= 75;
                    label.Text = money.ToString();
                    mode = ControllerState.NORMAL;
                }
            }

            if (mstate.RightButton == ButtonState.Pressed && selectedEntity != null && mode == ControllerState.NORMAL)
            {
                selectedEntity.receiveCommand(new MoveCommand((int)mousePosition.X, (int)mousePosition.Y));
            }
        }

        public void setMoney(int x)
        {
            label.Text = money.ToString();
            money = x;
        }

        public int getMoney()
        {
            return money;
        }
    }
}
