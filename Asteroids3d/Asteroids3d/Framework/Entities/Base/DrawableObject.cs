// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Base.DrawableObject
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d.Framework.Entities.Base
{
  public class DrawableObject
  {
    protected Model Model { get; }

    protected bool EnableDepthBuffer { get; set; }

    protected DrawableObject(string modelName) => this.Model = Asteroids3D.Instance.Content.Load<Model>("Models/" + modelName);

    protected virtual void LightingEffects(ref BasicEffect basicEffect)
    {
      basicEffect.EnableDefaultLighting();
      basicEffect.PreferPerPixelLighting = true;
    }

    protected virtual Matrix WorldTransformation() => Matrix.Identity;

    public virtual void Draw(Camera camera)
    {
      foreach (ModelMesh mesh in this.Model.Meshes)
      {
        foreach (Effect effect in mesh.Effects)
        {
          if (effect is BasicEffect basicEffect)
          {
            this.LightingEffects(ref basicEffect);
            basicEffect.World = this.WorldTransformation();
            basicEffect.View = camera.View;
            basicEffect.Projection = camera.Projection;
            basicEffect.GraphicsDevice.DepthStencilState = this.EnableDepthBuffer ? DepthStencilState.Default : DepthStencilState.None;
          }
        }
        mesh.Draw();
      }
    }
  }
}
