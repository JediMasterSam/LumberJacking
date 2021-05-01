// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Input.MouseController
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids3d.Framework.Input
{
  public class MouseController
  {
    private static MouseController _mouseController;
    private const float Tolerance = 0.5f;

    public static MouseController Instance => MouseController._mouseController ?? (MouseController._mouseController = new MouseController());

    private MouseController()
    {
      this.Center = new Vector2((float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth / 2f, (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight / 2f);
      this.Center = this.Center;
      this.Axis = Vector2.Zero;
      this.ResetPosition();
    }

    private Vector2 Center { get; }

    public Vector2 Axis { get; private set; }

    public bool Left => (double) this.Axis.X < -0.5;

    public bool Right => (double) this.Axis.X > 0.5;

    public bool Up => (double) this.Axis.Y > 0.5;

    public bool Down => (double) this.Axis.Y < -0.5;

    public void Update()
    {
      MouseState state = Mouse.GetState();
      this.Axis = new Vector2((float) state.X, (float) state.Y) - this.Center;
      this.Axis = new Vector2(this.Left || this.Right ? this.Axis.X : 0.0f, this.Up || this.Down ? this.Axis.Y : 0.0f);
      this.ResetPosition();
    }

    private void ResetPosition() => Mouse.SetPosition((int) this.Center.X, (int) this.Center.Y);
  }
}
