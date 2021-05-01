// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.Menus.MenuState
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Sounds;
using Asteroids3d.Framework.States.Base;
using Asteroids3d.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids3d.Framework.States.Menus
{
  public abstract class MenuState : IState
  {
    protected List<Button> Buttons { get; }

    private string Title { get; }

    private Vector2 TitlePosition { get; }

    protected MenuState(string title)
    {
      this.Title = title;
      Vector2 vector2 = Asteroids3D.Instance.Font.MeasureString(this.Title);
      float num1 = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth * 0.5f;
      float num2 = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.3f;
      this.TitlePosition = new Vector2(num1 - vector2.X / 2f, num2 - vector2.Y / 2f);
      this.Buttons = new List<Button>();
    }

    public virtual void Initialize()
    {
      Asteroids3D.Instance.IsMouseVisible = true;
      SoundManager.Instance.PauseAll();
    }

    public void Update(GameTime gameTime)
    {
      MouseState mouseState = Mouse.GetState(Asteroids3D.Instance.Window);
      this.Buttons.ForEach((Action<Button>) (button => button.Update(mouseState)));
    }

    public void Draw(GameTime gameTime)
    {
      Asteroids3D.Instance.SpriteBatch.Begin();
      Asteroids3D.Instance.SpriteBatch.DrawString(Asteroids3D.Instance.Font, this.Title, this.TitlePosition, Color.White);
      this.Buttons.ForEach((Action<Button>) (button => button.Draw(Asteroids3D.Instance.SpriteBatch)));
      Asteroids3D.Instance.SpriteBatch.End();
    }
  }
}
