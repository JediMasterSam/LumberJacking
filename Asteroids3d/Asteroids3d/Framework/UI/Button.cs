// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Objects.UI.Button
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids3d.Objects.UI
{
  public class Button
  {
    public Button(string text, Asteroids3d.Framework.States.Base.State setState, Vector2 position, Color none, Color hover)
    {
      this.Text = text;
      this.SetState = setState;
      this.Colors = new Color[3]{ none, hover, hover };
      Vector2 vector2 = Asteroids3D.Instance.Font.MeasureString(text);
      this.Rectangle = new Rectangle((int) position.X - (int) vector2.X / 2, (int) position.Y - (int) vector2.Y / 2, (int) vector2.X, (int) vector2.Y);
    }

    public string Text { get; }

    public Rectangle Rectangle { get; }

    private Asteroids3d.Framework.States.Base.State SetState { get; }

    private Button.GameButtonState State { get; set; }

    private Color[] Colors { get; }

    public void Update(MouseState mouseState)
    {
      Button.GameButtonState state = this.State;
      this.State = this.Rectangle.Contains(mouseState.X, mouseState.Y) ? (mouseState.LeftButton == ButtonState.Pressed ? Button.GameButtonState.Pressed : Button.GameButtonState.Hover) : Button.GameButtonState.None;
      if (state != Button.GameButtonState.Pressed || this.State != Button.GameButtonState.Hover)
        return;
      this.State = Button.GameButtonState.None;
      Asteroids3D.Instance.StateManager.SetState(this.SetState);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      SpriteBatch spriteBatch1 = spriteBatch;
      SpriteFont font = Asteroids3D.Instance.Font;
      string text = this.Text;
      Rectangle rectangle = this.Rectangle;
      double left = (double) rectangle.Left;
      rectangle = this.Rectangle;
      double top = (double) rectangle.Top;
      Vector2 position = new Vector2((float) left, (float) top);
      Color color = this.Colors[(int) this.State];
      spriteBatch1.DrawString(font, text, position, color);
    }

    public enum GameButtonState
    {
      None,
      Pressed,
      Hover,
    }
  }
}
