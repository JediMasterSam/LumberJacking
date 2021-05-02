using LumberJacking.GameObject;
using LumberJacking.GameObject.Components;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumberJacking.Input;

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
            var gameInstance = LumberJackingGame.Instance;

            if (gameInstance.Input.IsActive(Input.PlayerAction.RotateLeft)) Transform.Rotation -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.005f;
            if (gameInstance.Input.IsActive(Input.PlayerAction.RotateRight)) Transform.Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.005f;

            var localForward = GetLookAtVector();
            localForward.Normalize();

            localForward *= gameInstance.Input.IsActive(PlayerAction.Run) ? 10 : 5;

            var localRight = new Vector3(localForward.Z, 0, -localForward.X);
            var localLeft = new Vector3(-localForward.Z, 0, localForward.X);

            if (gameInstance.Input.IsActive(Input.PlayerAction.Forward)) Transform.Position += localForward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Backward)) Transform.Position += -localForward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Left)) Transform.Position += localRight * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Right)) Transform.Position += localLeft * (float)gameTime.ElapsedGameTime.TotalSeconds;

            View = Matrix.CreateLookAt(Transform.Position, Transform.Position + GetLookAtVector(), Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane);

            base.Update(gameTime);
        }

        private Vector3 GetLookAtVector()
        {
            var angle = Transform.Rotation;
            return new Vector3(MathF.Cos(angle * DegToRad), 0, MathF.Sin(angle * DegToRad));
        }
    }
}
