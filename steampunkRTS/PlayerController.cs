using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Attributes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
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
            PLACING_FABRICATOR
        }

        ICommandable selectedEntity;

        public List<TextButton> buttons;
        public List<TextButton> globalButtons;

        public List<IEntity> entities;

        public Grid grid;

        public int money = 100;

        public Label label;

        private ControllerState mode;

        private Texture2D factory;
        private Texture2D troop;

        public PlayerController(Grid grid, List<IEntity> entities, ContentManager content)
        {
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
                if (money > 20)
                {
                    mode = ControllerState.PLACING_FACTORY;
                }
            };

            globalButtons.Add(button);

            Grid.SetColumn(button, 6);
            grid.Widgets.Add(button);

            factory = content.Load<Texture2D>("factorytest");
            troop = content.Load<Texture2D>("trooptest");
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
            if (mstate.LeftButton == ButtonState.Pressed)
            {
                if (mode == ControllerState.NORMAL) { 
                    bool shouldDeselect = true;

                    foreach (IEntity entity in entities)
                    {
                        ICommandable commandable = entity as ICommandable;

                        if (commandable != null)
                        {
                            if (commandable.getBoundingBox().Contains(new Vector2(mstate.X, mstate.Y)))
                            {
                                selectedEntity = commandable;
                                generateButtons(selectedEntity);
                                shouldDeselect = false;
                            }
                        }   
                    }

                    foreach (TextButton button in buttons)
                    {
                        if (button.ContainsGlobalPoint(new Point(mstate.X, mstate.Y)))
                        {
                            shouldDeselect = false;
                        }
                    }
                    foreach (TextButton button in globalButtons)
                    {
                        if (button.ContainsGlobalPoint(new Point(mstate.X, mstate.Y)))
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
                    newFactory.x = mstate.X;
                    newFactory.targetX = mstate.X;

                    newFactory.y = mstate.Y;
                    newFactory.targetY = mstate.Y;

                    newFactory.texture = factory;
                    newFactory.troopTexture = troop;

                    entities.Add(newFactory);

                    money -= 15;
                    mode = ControllerState.NORMAL;
                }
            }

            if (mstate.RightButton == ButtonState.Pressed && selectedEntity != null && mode == ControllerState.NORMAL)
            {
                selectedEntity.receiveCommand(new MoveCommand(mstate.X, mstate.Y));
            }
        }
    }
}
