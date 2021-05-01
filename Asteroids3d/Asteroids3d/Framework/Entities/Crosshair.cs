// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Crosshair
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids3d.Framework.Entities
{
  public class Crosshair : DrawableObject
  {
    private const string ModelName = "crosshair";

    public Crosshair(Ship ship)
      : base("crosshair")
    {
      this.Ship = ship;
      this.TargetLock = false;
      this.EnableDepthBuffer = false;
    }

    private bool TargetLock { get; set; }

    private Ship Ship { get; }

    public void SetTargetLock(IEnumerable<Asteroid> asteroids)
    {
      Ray ray = new Ray(this.Ship.PhysicsObject.Position, this.Ship.PhysicsObject.Forward);
      this.TargetLock = asteroids.Any<Asteroid>((Func<Asteroid, bool>) (asteroid => asteroid.PhysicsObject.BoundingSphere.Intersects(ray).HasValue));
    }

    protected override void LightingEffects(ref BasicEffect basicEffect)
    {
      Vector3 vector3 = (this.TargetLock ? Color.Red : Color.White).ToVector3();
      basicEffect.AmbientLightColor = vector3;
      basicEffect.DiffuseColor = vector3;
      basicEffect.EmissiveColor = vector3;
    }

    protected override Matrix WorldTransformation() => this.Ship.PhysicsObject.WorldTransform * Matrix.CreateTranslation(this.Ship.PhysicsObject.Forward * 15f);
  }
}
