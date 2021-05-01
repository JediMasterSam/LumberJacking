using LumberJacking.GameObject;
using LumberJacking.GameObject.Components;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.Objects
{
    public class Camera : BaseGameObject
    {
        private const float DegToRad = 180f / MathF.PI * 2;

        public Camera(float fieldOfView, float nearClipPlane = 1f, float farClipPlane = 500f) : base(LumberJackingGame.Instance)
        {
            var physics = AddComponent<BasicPhysics>(this);
            physics.GameObject = this;
            physics.Circle = new Circle(new Vector2(Transform.Position.X, Transform.Position.Z), 1f);

            Drawable = false;

            FieldOfView = fieldOfView;
            NearClipPlane = nearClipPlane;
            FarClipPlane = farClipPlane;
            View = Matrix.CreateLookAt(Transform.Position, Transform.Position + Vector3.Forward, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane);
        }

        public static float AspectRatio { get => LumberJackingGame.Instance.GraphicsDevice.Viewport.AspectRatio; }
        public float FieldOfView { get; set; }
        public float NearClipPlane { get; set; }
        public float FarClipPlane { get; set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public override void Update(GameTime gameTime)
        {
            if (LumberJackingGame.Instance.Input.IsActive(Input.PlayerAction.Forward)) Transform.Position += Vector3.Backward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (LumberJackingGame.Instance.Input.IsActive(Input.PlayerAction.Backward)) Transform.Position += Vector3.Forward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (LumberJackingGame.Instance.Input.IsActive(Input.PlayerAction.Left)) Transform.Position += Vector3.Right * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (LumberJackingGame.Instance.Input.IsActive(Input.PlayerAction.Right)) Transform.Position += Vector3.Left * (float)gameTime.ElapsedGameTime.TotalSeconds;

            View = Matrix.CreateLookAt(Transform.Position, Transform.Position + GetLookAtVector(), Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane);

            base.Update(gameTime);
        }

        private Vector3 GetLookAtVector()
        {
            var angle = Transform.Rotation;
            return new Vector3(MathF.Cos(MathF.Cos(angle * DegToRad)), Transform.Position.Y, MathF.Sin(angle * DegToRad));
        }
    }
}
