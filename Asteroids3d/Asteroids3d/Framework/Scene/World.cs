// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Scene.World
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Microsoft.Xna.Framework;
using System;

namespace Asteroids3d.Framework.Scene
{
  public class World
  {
    private BoundingSphere Sphere { get; }

    public float WarningDistance { get; }

    public float Radius { get; }

    public float SafeRadius { get; }

    private float RadiusSquared { get; }

    private float SafeRadiusSquared { get; }

    private float Range { get; }

    private float SafeRange { get; }

    private float Minimum { get; }

    private Random Random { get; }

    public World(float radius, float warningDistance)
    {
      this.Sphere = new BoundingSphere(Vector3.Zero, radius);
      this.WarningDistance = warningDistance;
      this.Radius = radius;
      this.SafeRadius = this.Sphere.Radius - warningDistance;
      this.RadiusSquared = radius * radius;
      this.SafeRadiusSquared = this.SafeRadius * this.SafeRadius;
      this.Range = radius * 2f;
      this.SafeRange = this.SafeRadius * 2f;
      this.Minimum = -radius;
      this.Random = new Random();
    }

    public Vector3 GetRandomPoint(bool isSafe = false)
    {
      float num1 = isSafe ? this.SafeRange : this.Range;
      float num2 = isSafe ? this.Minimum + this.WarningDistance : this.Minimum;
      float num3 = isSafe ? this.SafeRadiusSquared : this.RadiusSquared;
      float x;
      float y;
      float z;
      Vector3 vector3;
      do
      {
        x = (float) this.Random.NextDouble() * num1 + num2;
        y = (float) this.Random.NextDouble() * num1 + num2;
        z = (float) this.Random.NextDouble() * num1 + num2;
        vector3 = new Vector3(x, y, z);
      }
      while ((double) x * (double) x + (double) y * (double) y + (double) z * (double) z > (double) num3);
      return vector3;
    }

    public bool IsInside(GameObject gameObject) => (double) this.Radius > (double) gameObject.PhysicsObject.Position.Length();

    public float DistanceToBoundry(GameObject gameObject) => this.Sphere.Radius - gameObject.PhysicsObject.Position.Length();
  }
}
