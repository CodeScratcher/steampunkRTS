using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace steampunkRTS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IEntity> entities;

        IEntity selectedEntity = null;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            entities = new List<IEntity>(); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Troop troop = new Troop();

            troop.texture = this.Content.Load<Texture2D>("trooptest");

            entities.Add(troop);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            MouseState mstate = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            entityTick(kstate, mstate);
            commandEntities(mstate);

            base.Update(gameTime);
        }

        private void commandEntities(MouseState mstate)
        {
            if (mstate.RightButton == ButtonState.Pressed && selectedEntity != null)
            {
                ICommandable commandable = selectedEntity as ICommandable;

                commandable.receiveCommand(Command.MOVE, mstate.X, mstate.Y);
            }
        }

        void entityTick(KeyboardState kstate, MouseState mstate)
        {
            bool shouldDeselect = true;
            
            foreach (IEntity entity in entities)
            {
                entity.update(kstate, mstate);

                ICommandable commandable = entity as ICommandable;

                if (commandable != null)
                {
                    if (mstate.LeftButton == ButtonState.Pressed && commandable.getBoundingBox().Contains(mstate.Position))
                    {
                        selectedEntity = entity;
                        shouldDeselect = false;
                    }
                }
            }

            if (shouldDeselect && mstate.LeftButton == ButtonState.Pressed)
            {
                selectedEntity = null;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            renderEntities();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void renderEntities()
        {
            foreach (IEntity entity in entities)
            {
                IRenderableEntity renderableEntity = entity as IRenderableEntity;
                if (renderableEntity != null)
                {
                    renderableEntity.render(_spriteBatch);
                }
            }
        }
    }
}