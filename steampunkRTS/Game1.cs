﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Myra;
using Myra.Graphics2D.UI;

namespace steampunkRTS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IEntity> entities;

        private PlayerController playerController;

        private Desktop _desktop;

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

            MyraEnvironment.Game = this;

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            _desktop = new Desktop();
            _desktop.Root = grid;

            TextureMap map = new TextureMap();

            map.Add(TextureID.FACTORY, Content.Load<Texture2D>("factorytest"));
            map.Add(TextureID.MINE, Content.Load<Texture2D>("minetest"));
            map.Add(TextureID.TROOP, Content.Load<Texture2D>("trooptest"));

            playerController = new PlayerController(grid, entities, map);

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            MouseState mstate = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            entityTick(kstate, mstate, gameTime);
            playerController.commandEntities(kstate, mstate);

            base.Update(gameTime);
        }

        void entityTick(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {   
            foreach (IEntity entity in entities)
            {
                entity.update(kstate, mstate, gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            renderEntities();
            _spriteBatch.End();

            _desktop.Render();

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