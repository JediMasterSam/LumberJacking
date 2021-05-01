// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Base.GameObject
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d.Framework.Entities.Base
{
  public class GameObject : DrawableObject
  {
    public Asteroids3d.Framework.Entities.Base.PhysicsObject<GameObject> PhysicsObject { get; }

    protected Level Level { get; }

    protected bool EnableWarningLight { get; set; }

    public GameObject(string modelName, Level level)
      : base(modelName)
    {
      BoundingSphere original = this.Model.Meshes[0].BoundingSphere;
      for (int index = 1; index < this.Model.Meshes.Count; ++index)
        original = BoundingSphere.CreateMerged(original, this.Model.Meshes[index].BoundingSphere);
      this.PhysicsObject = new Asteroids3d.Framework.Entities.Base.PhysicsObject<GameObject>(original.Radius, this)
      {
        CollisionEvent = new CollisionEvent<GameObject>(this.Collision)
      };
      this.EnableDepthBuffer = true;
      this.Level = level;
    }

    protected virtual void Collision(GameObject other)
    {
    }

    public virtual void Spawn() => this.Level.Spawn<GameObject>(this);

    public virtual void Destroy() => this.Level.Destroy<GameObject>(this);

    protected override void LightingEffects(ref BasicEffect basicEffect)
    {
      base.LightingEffects(ref basicEffect);
      if (!this.EnableWarningLight)
        basicEffect.AmbientLightColor = Color.DarkBlue.ToVector3();
      else
        basicEffect.AmbientLightColor = (double) this.Level.World.DistanceToBoundry(this) > (double) this.Level.World.WarningDistance ? Color.DarkBlue.ToVector3() : Color.Red.ToVector3();
    }

    protected override Matrix WorldTransformation() => this.PhysicsObject.WorldTransform;

    protected virtual void OnLeftWorld() => this.Destroy();

    public virtual void Update(float deltaTime)
    {
      if (this.Level.World.IsInside(this))
        return;
      this.OnLeftWorld();
    }
  }
}
