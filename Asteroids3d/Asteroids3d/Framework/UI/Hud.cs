// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.UI.Hud
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities;
using Asteroids3d.Framework.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d.Framework.UI
{
  public class Hud
  {
    private const string Text = "REMAINING: ";

    private Rectangle ShipHealthSource { get; }

    private Rectangle ShipHealthDestination { get; }

    private Texture2D[] Health { get; }

    private Texture2D Shield { get; }

    private Vector2 TextLocation { get; }

    private Vector2 TimerLocation { get; }

    public Hud()
    {
      this.Health = new Texture2D[3]
      {
        Asteroids3D.Instance.Content.Load<Texture2D>("HUD/health1"),
        Asteroids3D.Instance.Content.Load<Texture2D>("HUD/health2"),
        Asteroids3D.Instance.Content.Load<Texture2D>("HUD/health3")
      };
      this.Shield = Asteroids3D.Instance.Content.Load<Texture2D>("HUD/shield");
      this.ShipHealthSource = new Rectangle(0, 0, this.Shield.Width, this.Shield.Height);
      float num1 = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth / 6f / (float) this.ShipHealthSource.Width;
      float x = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth * 0.02f;
      float num2 = (float) ((double) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.959999978542328 - (double) this.ShipHealthSource.Height * (double) num1);
      this.ShipHealthDestination = new Rectangle((int) x, (int) num2, (int) ((double) this.ShipHealthSource.Width * (double) num1), (int) ((double) this.ShipHealthSource.Height * (double) num1));
      this.TextLocation = new Vector2(x, (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.04f);
      this.TimerLocation = new Vector2((float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth * 0.85f, (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.04f);
    }

    public void Draw(SpriteBatch spriteBatch, Ship ship, Level level, float timer)
    {
      for (int index = 0; index < ship.Health; ++index)
        spriteBatch.Draw(this.Health[index], this.ShipHealthDestination, new Rectangle?(this.ShipHealthSource), Color.White);
      if (ship.Shield)
        spriteBatch.Draw(this.Shield, this.ShipHealthDestination, new Rectangle?(this.ShipHealthSource), Color.White);
      spriteBatch.DrawString(Asteroids3D.Instance.Font, "REMAINING: " + (object) (level.Goal - ship.AsteroidsDestroyed), this.TextLocation, Color.White);
      int num1 = (int) ((double) timer / 60.0);
      int num2 = (int) ((double) timer % 60.0);
      string text = num1.ToString() + ":" + (num2 >= 10 ? (object) string.Concat((object) num2) : (object) ("0" + (object) num2));
      spriteBatch.DrawString(Asteroids3D.Instance.Font, text, this.TimerLocation, Color.White);
    }
  }
}
