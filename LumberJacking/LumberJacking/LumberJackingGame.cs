using LumberJacking.GameObject;
using LumberJacking.Objects;
using LumberJacking.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LumberJacking
{
    public class LumberJackingGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public LumberJackingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GameObjects = new List<BaseGameObject>();

            Level = new Level();

            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Instance), "Only one instance of LumberJackingGame can exist at a time.");
            }
        }

        public static LumberJackingGame Instance { get; private set; }
        public Camera Camera { get; private set; }
        public Level Level { get; }
        public List<BaseGameObject> GameObjects { get; }

        protected override void Initialize()
        {
            Camera = new Camera(0.78f);
            var spawn = Level.Markers.First(marker => marker.CellType == CellType.Spawn).Position;
            Camera.Transform.Position = new Vector3(spawn.X, 1f, spawn.Y);
            
            foreach(var line in Level.Walls)
            {
                GameObjects.Add(new Wall(line, 10f));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}