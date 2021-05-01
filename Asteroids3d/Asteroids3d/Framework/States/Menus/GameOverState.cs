// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.Menus.GameOverState
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.States.Base;
using Asteroids3d.Objects.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Asteroids3d.Framework.States.Menus
{
  public class GameOverState : MenuState
  {
    public GameOverState()
      : base("GAME OVER")
      => this.Buttons.AddRange((IEnumerable<Button>) new Button[1]
      {
        new Button("MAIN MENU", State.MainMenu, new Vector2((float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth * 0.5f, (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight * 0.6f), Color.White, Color.Yellow)
      });

    public override void Initialize() => Asteroids3D.Instance.IsMouseVisible = true;
  }
}
