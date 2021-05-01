// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.Menus.MainMenuState
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.States.Base;
using Asteroids3d.Objects.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Asteroids3d.Framework.States.Menus
{
  public class MainMenuState : MenuState
  {
    public MainMenuState()
      : base("ASTEROIDS 3D")
    {
      float x = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth * 0.5f;
      float y = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.6f;
      Button button1 = new Button("PLAY", State.Play, new Vector2(x, y), Color.White, Color.Yellow);
      Button button2 = new Button("DIFFICULTY", State.Difficulty, new Vector2(x, (float) (button1.Rectangle.Bottom + 50)), Color.White, Color.Yellow);
      Button button3 = new Button("EXIT", State.Exit, new Vector2(x, (float) (button2.Rectangle.Bottom + 50)), Color.White, Color.Yellow);
      this.Buttons.AddRange((IEnumerable<Button>) new Button[3]
      {
        button1,
        button2,
        button3
      });
    }
  }
}
