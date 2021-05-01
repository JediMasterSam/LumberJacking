// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Asteroid
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Scene;
using Asteroids3d.Framework.States.Base;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace Asteroids3d.Framework.Entities
{
  public class Asteroid : ExplodableObject
  {
    private const string ModelName = "asteroid";

    private static Random Random { get; } = new Random();

    private float Size { get; }

    public Asteroid(int size, Level level, bool spawnOnEdge = false)
      : base("asteroid" + (object) size, level)
    {
      do
      {
        if (!spawnOnEdge)
          this.PhysicsObject.Position = level.World.GetRandomPoint(true);
        else
          this.PhysicsObject.Position = Vector3.Normalize(Asteroid.\u003C\u002Ector\u003Eg__RandomVector7_0()) * level.World.SafeRadius;
      }
      while (!level.IsSafeToSpawn(this.PhysicsObject.BoundingSphere));
      Vector3 vector3 = !spawnOnEdge ? Vector3.Normalize(Asteroid.\u003C\u002Ector\u003Eg__RandomVector7_0()) : Vector3.Normalize(level.World.GetRandomPoint(true) - this.PhysicsObject.Position);
      float num;
      switch (level.Difficulty)
      {
        case Difficulty.Beginner:
          num = (float) Asteroid.Random.Next(0, 4);
          break;
        case Difficulty.Easy:
          num = (float) Asteroid.Random.Next(4, 10);
          break;
        case Difficulty.Medium:
          num = (float) Asteroid.Random.Next(10, 20);
          break;
        case Difficulty.Hard:
          num = (float) Asteroid.Random.Next(20, 50);
          break;
        case Difficulty.Expert:
          num = (float) Asteroid.Random.Next(50, 100);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.PhysicsObject.LinearVelocity = vector3 * num;
      this.PhysicsObject.AngularVelocity = Asteroid.\u003C\u002Ector\u003Eg__RandomVector7_0() * (float) Asteroid.Random.Next(1, 10);
      this.Size = (float) size;
    }

    private Asteroid(Asteroid asteroid)
      : base(nameof (asteroid) + (object) (float) ((double) asteroid.Size - 1.0), asteroid.Level)
    {
      this.PhysicsObject.Position = asteroid.PhysicsObject.Position;
      this.PhysicsObject.LinearVelocity = asteroid.PhysicsObject.LinearVelocity;
      this.PhysicsObject.AngularVelocity = asteroid.PhysicsObject.AngularVelocity;
      this.Size = asteroid.Size - 1f;
    }

    protected override void OnLeftWorld()
    {
      base.OnLeftWorld();
      this.Level.SpawnAsteroids(1, true);
    }

    protected override void Collision(GameObject other)
    {
      switch (other)
      {
        case Laser _:
          this.Explode(Color.OrangeRed, maximumScale: this.PhysicsObject.Radius);
          break;
        case Asteroid _:
          if ((double) this.Size > 1.0)
            this.Level.Spawn<Asteroid>(new Asteroid(this));
          this.Explode(Color.Gray, maximumScale: this.PhysicsObject.Radius, playSound: false);
          break;
        default:
          base.Collision(other);
          break;
      }
    }

    [CompilerGenerated]
    internal static Vector3 \u003C\u002Ector\u003Eg__RandomVector7_0() => new Vector3((float) (Asteroid.Random.NextDouble() * 2.0 - 1.0), (float) (Asteroid.Random.NextDouble() * 2.0 - 1.0), (float) (Asteroid.Random.NextDouble() * 2.0 - 1.0));
  }
}
