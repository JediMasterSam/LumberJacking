// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Base.ExplodableObject
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Scene;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Asteroids3d.Framework.Entities.Base
{
  public class ExplodableObject : GameObject
  {
    protected Explosion Explosion { get; set; }

    private bool IsExploding { get; set; }

    public ExplodableObject(string modelName, Level level)
      : base(modelName, level)
    {
      Debug.WriteLine(modelName);
      this.IsExploding = false;
    }

    public override void Update(float deltaTime)
    {
      if (this.IsExploding && this.Explosion.IsComplete)
        this.Destroy();
      else
        base.Update(deltaTime);
    }

    public void Explode(Color color, float speed = 1f, float maximumScale = 10f, bool playSound = true)
    {
      this.IsExploding = true;
      this.Explosion = new Explosion(this.PhysicsObject.Position, speed, maximumScale, color, playSound);
      this.Level.StopPhysics((GameObject) this);
    }

    public override void Draw(Camera camera)
    {
      if (this.IsExploding)
      {
        this.Explosion.Update();
        this.Explosion.Draw(camera);
      }
      else
        base.Draw(camera);
    }
  }
}
