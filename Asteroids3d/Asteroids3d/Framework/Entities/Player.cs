// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Player
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Input;
using Asteroids3d.Framework.Scene;
using Asteroids3d.Framework.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids3d.Framework.Entities
{
  public class Player : Ship
  {
    private const string AlertSound = "alert";
    private const string Ambience = "ambience";
    private const float ProximityDistance = 15f;
    private const float MaximumCameraLag = 1.5f;
    private const float CameraLagSpeed = 0.01f;

    private float CameraLag { get; set; }

    public Player(Level level)
      : base(level)
    {
      this.Rotation = new Func<Vector3>(Player.GetRotation);
      this.Movement = new Func<bool>(this.GetMovement);
      this.Attack = new Func<bool>(Player.GetAttack);
      this.Crosshair = new Crosshair((Ship) this);
      this.Level.Camera.Offset = new Vector3(0.0f, 1f, 6f);
      this.Level.Camera.Target = (GameObject) this;
      SoundManager.Instance.Play("ambience", true);
      this.EnableWarningLight = true;
    }

    private Crosshair Crosshair { get; }

    private static Vector3 GetRotation()
    {
      Vector2 axis = MouseController.Instance.Axis;
      int num = Actions.Instance.Check("rollLeft") ? -1 : (Actions.Instance.Check("rollRight") ? 1 : 0);
      return new Vector3(-axis.Y, -axis.X, (float) num);
    }

    private bool GetMovement()
    {
      if (Actions.Instance.Check("thrusters"))
      {
        this.CameraLag += 0.01f;
        if ((double) this.CameraLag > 1.5)
          this.CameraLag = 1.5f;
        return true;
      }
      this.CameraLag -= 0.01f;
      if ((double) this.CameraLag < 0.0)
        this.CameraLag = 0.0f;
      return false;
    }

    private static bool GetAttack() => Actions.Instance.Check("torpedo");

    public override void Update(float deltaTime)
    {
      Actions.Instance.Update(Asteroids3D.Instance.StateManager.KeyboardState, Mouse.GetState());
      base.Update(deltaTime);
      this.ProximityAlert();
      this.Crosshair.SetTargetLock((IEnumerable<Asteroid>) this.Level.Asteroids);
      this.Level.Camera.Translation = this.PhysicsObject.Up + this.PhysicsObject.Backward * this.CameraLag;
    }

    public override void Draw(Camera camera)
    {
      base.Draw(camera);
      if (this.Health <= 0)
        return;
      this.Crosshair.Draw(camera);
    }

    private void ProximityAlert()
    {
      if (this.Level.Asteroids.Select<Asteroid, float>((Func<Asteroid, float>) (asteroid => (asteroid.PhysicsObject.Position - this.PhysicsObject.Position).Length() - asteroid.PhysicsObject.Radius)).Any<float>((Func<float, bool>) (distance => (double) distance <= 15.0)))
      {
        if (SoundManager.Instance.State("alert") == SoundState.Playing)
          return;
        SoundManager.Instance.Play("alert", true);
      }
      else
      {
        if (SoundManager.Instance.State("alert") != SoundState.Playing)
          return;
        SoundManager.Instance.Stop("alert");
      }
    }
  }
}
