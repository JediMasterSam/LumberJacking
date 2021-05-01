// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Asteroids3D
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d
{
  public class Asteroids3D : Game
  {
    private static Asteroids3D _instance;

    public static Asteroids3D Instance => Asteroids3D._instance ?? (Asteroids3D._instance = new Asteroids3D());

    public GraphicsDeviceManager Graphics { get; }

    public SpriteBatch SpriteBatch { get; private set; }

    public StateManager StateManager { get; private set; }

    public SpriteFont Font { get; private set; }

    private Asteroids3D()
    {
      this.Graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      this.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
      this.Graphics.PreferMultiSampling = true;
      this.GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
      this.Graphics.ApplyChanges();
      this.Graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
      this.Graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
      this.Graphics.IsFullScreen = true;
      this.Graphics.ApplyChanges();
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
      this.Font = this.Content.Load<SpriteFont>("Fonts/Font");
      this.StateManager = new StateManager();
    }

    protected override void Update(GameTime gameTime)
    {
      this.StateManager.Update(gameTime);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      this.GraphicsDevice.Clear(Color.Black);
      this.StateManager.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
