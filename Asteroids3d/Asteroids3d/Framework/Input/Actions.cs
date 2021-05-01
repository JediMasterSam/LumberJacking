// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Input.Actions
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Asteroids3d.Framework.Input
{
  public class Actions
  {
    private static Actions _actions;

    public static Actions Instance => Actions._actions ?? (Actions._actions = new Actions());

    private Dictionary<string, ActionBinding> ActionBindings { get; }

    private KeyboardState Keyboad { get; set; }

    private MouseState Mouse { get; set; }

    private Actions() => this.ActionBindings = new Dictionary<string, ActionBinding>()
    {
      {
        "thrusters",
        (ActionBinding) ((keboard, mouse) => keboard.IsKeyDown(Keys.Space))
      },
      {
        "torpedo",
        (ActionBinding) ((keboard, mouse) => mouse.LeftButton == ButtonState.Pressed)
      },
      {
        "rollLeft",
        (ActionBinding) ((keboard, mouse) => keboard.IsKeyDown(Keys.Q))
      },
      {
        "rollRight",
        (ActionBinding) ((keboard, mouse) => keboard.IsKeyDown(Keys.E))
      }
    };

    public void Update(KeyboardState keyboard, MouseState mouse)
    {
      this.Keyboad = keyboard;
      this.Mouse = mouse;
    }

    public bool Check(string actionName)
    {
      ActionBinding actionBinding;
      return this.ActionBindings.TryGetValue(actionName, out actionBinding) && actionBinding(this.Keyboad, this.Mouse);
    }
  }
}
