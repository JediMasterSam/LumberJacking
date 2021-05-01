// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Base.SpaceHelper
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using BEPUphysics;

namespace Asteroids3d.Framework.Entities.Base
{
  public static class SpaceHelper
  {
    public static void Add<T>(this Space space, PhysicsObject<T> physicsObject) where T : class => physicsObject.Add(space);

    public static void Remove<T>(this Space space, PhysicsObject<T> physicsObject) where T : class => physicsObject.Remove(space);
  }
}
