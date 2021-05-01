// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Entities.Explosion
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids3d.Framework.Entities
{
  public class Explosion : DrawableObject
  {
    private const string ModelName = "sphere";
    private const string SoundName = "explosion";

    private Vector3 Position { get; }

    private float Scale { get; set; }

    private float Speed { get; }

    private float MaximumScale { get; }

    public bool IsComplete { get; private set; }

    private Color Color { get; }

    public Explosion(
      Vector3 position,
      float speed,
      float maximumScale,
      Color color,
      bool playSound)
      : base("sphere")
    {
      this.Position = position;
      this.Scale = 0.0f;
      this.Speed = speed;
      this.MaximumScale = maximumScale;
      this.IsComplete = false;
      this.Color = color;
      if (!playSound)
        return;
      SoundManager.Instance.Play("explosion", volume: 0.25f);
    }

    public void Update()
    {
      if ((double) this.Scale < (double) this.MaximumScale)
        this.Scale += this.Speed;
      else
        this.IsComplete = true;
    }

    protected override Matrix WorldTransformation() => Matrix.CreateScale(this.Scale) * Matrix.CreateTranslation(this.Position);

    protected override void LightingEffects(ref BasicEffect basicEffect)
    {
      basicEffect.EnableDefaultLighting();
      basicEffect.PreferPerPixelLighting = true;
      basicEffect.AmbientLightColor = this.Color.ToVector3();
      basicEffect.EmissiveColor = this.Color.ToVector3();
      basicEffect.Alpha = 0.75f;
    }
  }
}
