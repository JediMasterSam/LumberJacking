// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.Base.IState
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework;

namespace Asteroids3d.Framework.States.Base
{
  public interface IState
  {
    void Initialize();

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime);
  }
}
