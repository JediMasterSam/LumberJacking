// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Scene.Camera
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities.Base;
using Microsoft.Xna.Framework;

namespace Asteroids3d.Framework.Scene
{
  public class Camera
  {
    private static float AspectRatio { get; } = (float) Asteroids3D.Instance.Graphics.PreferredBackBufferWidth / (float) Asteroids3D.Instance.Graphics.PreferredBackBufferHeight;

    public Vector3 Position { get; private set; }

    public Matrix View { get; private set; }

    public Matrix Projection { get; }

    public GameObject Target { get; set; }

    public Vector3 Offset { get; set; }

    public Vector3 Translation { get; set; }

    public Camera(Vector3 position, float nearClipPlane = 1f, float farClipPlane = 500f)
    {
      this.Position = position;
      this.View = Matrix.CreateLookAt(position, position + Vector3.Forward, Vector3.Up);
      this.Projection = Matrix.CreatePerspectiveFieldOfView(0.7853982f, Camera.AspectRatio, nearClipPlane, farClipPlane);
    }

    public Camera(Vector3 position, Vector3 target, float nearClipPlane = 1f, float farClipPlane = 200f)
    {
      this.Position = position;
      this.View = Matrix.CreateLookAt(position, target, Vector3.Up);
      this.Projection = Matrix.CreatePerspectiveFieldOfView(0.7853982f, Camera.AspectRatio, nearClipPlane, farClipPlane);
    }

    public void Update()
    {
      if (this.Target == null)
        return;
      Vector3 cameraTarget = this.Target.PhysicsObject.Position + this.Translation;
      this.Position += cameraTarget + Vector3.Transform(this.Offset, this.Target.PhysicsObject.Orientation) - this.Position;
      this.View = Matrix.CreateLookAt(this.Position, cameraTarget, this.Target.PhysicsObject.Up);
    }
  }
}
