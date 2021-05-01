using LumberJacking.GameObject;
using LumberJacking.Input;
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
            Input = new InputManager();

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
        public InputManager Input { get; }

        //temp
        public Texture2D WallTexture { get; set; }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            var spawn = Level.Markers.First(marker => marker.CellType == CellType.Spawn).Position;
            Camera.Transform.Position = new Vector3(spawn.X, 0.5f, spawn.Y);
            Camera.Transform.Rotation = 0;

            GameObjects.Add(Camera);

            base.Initialize();

            foreach (var line in Level.Walls)
            {
                GameObjects.Add(new Wall(line, WallTexture, 1f));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var WallTexture = Content.Load<Texture2D>("wall_texture");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update();

            foreach(var gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}