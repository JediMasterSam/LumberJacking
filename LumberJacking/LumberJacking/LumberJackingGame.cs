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

            if (Instance == null)
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
        public Texture2D AxeTexture { get; set; }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();

            var spawn = Level.Markers.First(marker => marker.CellType == CellType.Spawn).Position;
            Camera = new Camera(0.4f, new Vector3(spawn.X, 0.5f, spawn.Y), AxeTexture);
            Camera.Transform.Rotation = 180;

            GameObjects.Add(Camera);

            foreach (var line in Level.Walls)
            {
                GameObjects.Add(new Wall(line, WallTexture, 1f));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            WallTexture = Content.Load<Texture2D>("wall_texture");
            AxeTexture = Content.Load<Texture2D>("BFA");

            var tree1 = Content.Load<Texture2D>("tree_1");

            foreach (var levelMarker in Level.Markers)
            {
                if (levelMarker.CellType == CellType.Enemy)
                {
                    GameObjects.Add(new Enemy(levelMarker.Position, tree1, 1));
                }
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update();

            for (var index = 0; index < GameObjects.Count; index++)
            {
                var gameObject = GameObjects[index];
                gameObject.Update(gameTime);
                if (gameObject.Delete)
                {
                    GameObjects.Remove(gameObject);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(12829635));

            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}