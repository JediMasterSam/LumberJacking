// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Ship
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Scene;
using Asteroids3d.Framework.Sounds;
using Microsoft.Xna.Framework;
using System;

namespace Asteroids3d.Framework.Entities
{
  public class Ship : ExplodableObject
  {
    private const string ModelName = "ship";
    private const string SoundName = "engine";
    private const float RotationSpeed = 0.03f;
    private const float MovementSpeed = 1.5f;
    private const float MaximumRotationSpeed = 5f;
    private const float MaximumMovementSpeed = 50f;
    private const float RateOfFire = 0.5f;
    private const float MaximumHealth = 3f;
    private const float ShieldRegenerationTime = 10f;

    protected Func<Vector3> Rotation { get; set; }

    protected Func<bool> Movement { get; set; }

    protected Func<bool> Attack { get; set; }

    private float LaserTimer { get; set; }

    private float ShieldTimer { get; set; }

    public int Health { get; private set; }

    public bool Shield { get; private set; }

    public int AsteroidsDestroyed { get; set; }

    protected Ship(Level level)
      : base("ship", level)
    {
      this.PhysicsObject.AngularDamping = 0.95f;
      this.PhysicsObject.LinearDamping = 0.95f;
      this.LaserTimer = 0.5f;
      this.Rotation = (Func<Vector3>) (() => Vector3.Zero);
      this.Movement = (Func<bool>) (() => false);
      this.Attack = (Func<bool>) (() => false);
      this.Health = 3;
      this.Shield = true;
      this.AsteroidsDestroyed = 0;
    }

    public override void Update(float deltaTime)
    {
      base.Update(deltaTime);
      Vector3 vector3 = this.Rotation() * 0.03f;
      bool flag1 = this.Movement();
      bool flag2 = this.Attack();
      this.PhysicsObject.AngularVelocity += Vector3.Transform(vector3, this.PhysicsObject.Orientation);
      if ((double) this.PhysicsObject.AngularVelocity.Length() > 5.0)
        this.PhysicsObject.AngularVelocity = Vector3.Normalize(this.PhysicsObject.AngularVelocity) * 5f;
      if (flag1)
      {
        this.PhysicsObject.LinearVelocity += Vector3.Transform(Vector3.Forward * 1.5f, this.PhysicsObject.Orientation);
        if ((double) this.PhysicsObject.LinearVelocity.Length() > 50.0)
          this.PhysicsObject.LinearVelocity = Vector3.Normalize(this.PhysicsObject.LinearVelocity) * 50f;
        if ((uint) SoundManager.Instance.State("engine") > 0U)
          SoundManager.Instance.Play("engine", true, 0.1f);
      }
      else
        SoundManager.Instance.Stop("engine");
      if (flag2 && (double) this.LaserTimer >= 0.5)
      {
        new Laser(this.Level, this).Spawn();
        this.LaserTimer = 0.0f;
      }
      else
        this.LaserTimer += deltaTime;
      if (this.Shield)
        return;
      this.ShieldTimer += deltaTime;
      if ((double) this.ShieldTimer >= 10.0)
      {
        this.ShieldTimer = 0.0f;
        this.Shield = true;
      }
    }

    protected override void OnLeftWorld()
    {
      this.Shield = false;
      this.Health = 0;
      this.Explode(Color.OrangeRed);
    }

    protected override void Collision(GameObject other)
    {
      if (this.Shield)
      {
        this.Shield = false;
      }
      else
      {
        --this.Health;
        if (this.Health > 0)
          return;
        this.Health = 0;
        this.Explode(Color.OrangeRed);
      }
    }
  }
}
