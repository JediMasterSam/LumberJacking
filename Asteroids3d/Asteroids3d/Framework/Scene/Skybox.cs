// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Scene.Skybox
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d.Framework.Scene
{
  public class Skybox : DrawableObject
  {
    private const string ModelName = "skybox";
    private const float Scale = 100f;

    private Level Level { get; }

    public GameObject Target { get; set; }

    public Skybox(Level level)
      : base("skybox")
      => this.Level = level;

    public override void Draw(Camera camera)
    {
      RasterizerState rasterizerState = Asteroids3D.Instance.Graphics.GraphicsDevice.RasterizerState;
      Asteroids3D.Instance.Graphics.GraphicsDevice.RasterizerState = new RasterizerState()
      {
        CullMode = CullMode.None
      };
      base.Draw(camera);
      Asteroids3D.Instance.Graphics.GraphicsDevice.RasterizerState = rasterizerState;
    }

    protected override Matrix WorldTransformation() => Matrix.CreateScale(100f) * Matrix.CreateTranslation(this.Level.Camera.Position);

    protected override void LightingEffects(ref BasicEffect basicEffect)
    {
      basicEffect.LightingEnabled = false;
      if (this.Target == null)
        basicEffect.DiffuseColor = Color.DarkSlateBlue.ToVector3();
      else
        basicEffect.DiffuseColor = (double) this.Level.World.DistanceToBoundry(this.Target) > (double) this.Level.World.WarningDistance ? Color.DarkSlateBlue.ToVector3() : Color.Red.ToVector3();
    }
  }
}
