// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Base.PhysicsObject`1
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Helpers;
using BEPUphysics;
using BEPUphysics.BroadPhaseEntries.Events;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.Entities;
using Microsoft.Xna.Framework;

namespace Asteroids3d.Framework.Entities.Base
{
  public class PhysicsObject<T> where T : class
  {
    public PhysicsObject(float radius, T tag)
    {
      this.Entity = new Entity((EntityShape) new SphereShape(radius), 1f);
      this.Entity.CollisionInformation.Events.ContactCreated += (ContactCreatedEventHandler<EntityCollidable>) ((sender, other, pair, contact) =>
      {
        Asteroids3d.Framework.Entities.Base.CollisionEvent<T> collisionEvent = this.CollisionEvent;
        if (collisionEvent == null)
          return;
        collisionEvent(other.Tag as T);
      });
      this.Radius = radius;
      this.Tag = tag;
      this.IsAlive = true;
    }

    private Entity Entity { get; }

    private bool IsAlive { get; set; }

    public float AngularDamping
    {
      get => this.Entity.AngularDamping;
      set => this.Entity.AngularDamping = value;
    }

    public float LinearDamping
    {
      get => this.Entity.LinearDamping;
      set => this.Entity.LinearDamping = value;
    }

    public Vector3 Position
    {
      get => MathConverter.Convert(this.Entity.Position);
      set => this.Entity.Position = MathConverter.Convert(value);
    }

    public Quaternion Orientation
    {
      get => MathConverter.Convert(this.Entity.Orientation);
      set => this.Entity.Orientation = MathConverter.Convert(value);
    }

    public Vector3 AngularVelocity
    {
      get => MathConverter.Convert(this.Entity.AngularVelocity);
      set => this.Entity.AngularVelocity = MathConverter.Convert(value);
    }

    public Vector3 LinearVelocity
    {
      get => MathConverter.Convert(this.Entity.LinearVelocity);
      set => this.Entity.LinearVelocity = MathConverter.Convert(value);
    }

    public Matrix WorldTransform
    {
      get => MathConverter.Convert(this.Entity.WorldTransform);
      set => this.Entity.WorldTransform = MathConverter.Convert(value);
    }

    public float Mass
    {
      get => this.Entity.Mass;
      set => this.Entity.Mass = value;
    }

    public T Tag
    {
      get => this.Entity.CollisionInformation.Tag as T;
      set => this.Entity.CollisionInformation.Tag = (object) value;
    }

    public Vector3 Forward => Vector3.Transform(Vector3.Forward, this.Orientation);

    public Vector3 Backward => Vector3.Transform(Vector3.Backward, this.Orientation);

    public Vector3 Left => Vector3.Transform(Vector3.Left, this.Orientation);

    public Vector3 Right => Vector3.Transform(Vector3.Right, this.Orientation);

    public Vector3 Up => Vector3.Transform(Vector3.Up, this.Orientation);

    public Vector3 Down => Vector3.Transform(Vector3.Down, this.Orientation);

    public Asteroids3d.Framework.Entities.Base.CollisionEvent<T> CollisionEvent { get; set; }

    public BoundingSphere BoundingSphere => new BoundingSphere(this.Position, this.Radius);

    public float Radius { get; }

    internal void Add(Space space) => space.Add((ISpaceObject) this.Entity);

    internal void Remove(Space space)
    {
      if (!this.IsAlive)
        return;
      space.Remove((ISpaceObject) this.Entity);
      this.IsAlive = false;
    }
  }
}
