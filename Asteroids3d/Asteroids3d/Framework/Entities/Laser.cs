// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Laser
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Scene;
using Asteroids3d.Framework.Sounds;

namespace Asteroids3d.Framework.Entities
{
  public class Laser : GameObject
  {
    private const string ModelName = "laser";
    private const string Sound = "torpedo";

    private Ship Ship { get; }

    public Laser(Level level, Ship ship)
      : base("laser", level)
    {
      this.Ship = ship;
      this.PhysicsObject.Mass = 1000f;
      this.PhysicsObject.Position = ship.PhysicsObject.Position + ship.PhysicsObject.Forward * (float) ((double) ship.PhysicsObject.BoundingSphere.Radius + (double) this.PhysicsObject.BoundingSphere.Radius + 1.0);
      this.PhysicsObject.Orientation = ship.PhysicsObject.Orientation;
      this.PhysicsObject.LinearVelocity = ship.PhysicsObject.Forward * 300f;
    }

    public override void Spawn()
    {
      base.Spawn();
      SoundManager.Instance.Play("torpedo", volume: 0.25f);
    }

    public override void Update(float deltaTime)
    {
      if (this.Level.World.IsInside((GameObject) this))
        return;
      this.Destroy();
    }

    protected override void Collision(GameObject other)
    {
      if (other is Asteroid)
        ++this.Ship.AsteroidsDestroyed;
      this.Destroy();
    }
  }
}
