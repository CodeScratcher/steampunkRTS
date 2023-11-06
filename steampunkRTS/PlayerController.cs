﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        ICommandable selectedEntity;

        public List<TextButton> buttons;

        public List<IEntity> entities;

        public Grid grid;
        
        public PlayerController(Grid grid, List<IEntity> entities)
        {
            buttons = new List<TextButton>();
            this.grid = grid;
            this.entities = entities;
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

            int i = 0;
            foreach (string str in entity.getGuiCommands())
            {
                TextButton button = new TextButton
                {
                    Text = str
                };

                button.Click += (s, a) =>
                {
                    entity.receiveCommand(Command.GUI_COMMAND, str, 0, 0, entities);
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

                if (shouldDeselect)
                {
                    removeButtons();
                    selectedEntity = null;
                }
            }

            if (mstate.RightButton == ButtonState.Pressed && selectedEntity != null)
            {
                selectedEntity.receiveCommand(Command.MOVE, null, mstate.X, mstate.Y, entities);
            }
        }
    }
}
