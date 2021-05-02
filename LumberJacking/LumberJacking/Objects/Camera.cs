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
using Microsoft.Xna.Framework.Graphics;

namespace LumberJacking.Objects
{
    public class Camera : BaseGameObject
    {
        private const float DegToRad = 180f / MathF.PI * 2;

        public Camera(float fieldOfView, Vector3 position, Texture2D texture, float nearClipPlane = 1f, float farClipPlane = 500f) : base(LumberJackingGame.Instance)
        {
            Transform.Position = position;
            MeshRenderer.Texture = texture;
            MeshRenderer.Effect = new AlphaTestEffect(GraphicsDevice);

            var physics = AddComponent<BasicPhysics>(this);
            physics.GameObject = this;
            physics.Circle = new Circle(new Vector2(Transform.Position.X, Transform.Position.Z), 1f);

            FieldOfView = fieldOfView;
            NearClipPlane = nearClipPlane;
            FarClipPlane = farClipPlane;
            View = Matrix.CreateLookAt(Transform.Position, Transform.Position + Vector3.Forward, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane);

            var point = new Vector2(Transform.Position.X, Transform.Position.Z);

            var offset = new Vector2(1f, 0);
            var start = point - offset;
            var end = point + offset;
            var line = new Line(start, end);
            var length = (end - start).Length();

            var height = position.Y - 0.25f;

            var vertices = new VertexPositionTexture[]
            {
                new(new Vector3(line.Start.X, height, line.Start.Y + 1f), new Vector2(0, 1)),
                new(new Vector3(line.Start.X, height + 0.25f, line.Start.Y + length + 1f), new Vector2(0, 0)),
                new(new Vector3(line.End.X, height + 0.25f, line.End.Y + length + 1f), new Vector2(1, 0)),
                new(new Vector3(line.End.X, height, line.End.Y + 1f), new Vector2(1, 1)),
            };

            var triangleIndices = new short[]
            {
                0,
                1,
                2,
                2,
                3,
                0
            };

            MeshRenderer.Verticies = vertices;
            MeshRenderer.TriangleIndices = triangleIndices;
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

            if (gameInstance.Input.IsActive(Input.PlayerAction.RotateLeft)) Transform.Rotation -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.01f;
            if (gameInstance.Input.IsActive(Input.PlayerAction.RotateRight)) Transform.Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.01f;

            var localForward = GetLookAtVector();
            localForward.Normalize();
            var localForwardNormalized = localForward;

            localForward *= gameInstance.Input.IsActive(PlayerAction.Run) ? 10 : 5;

            var localRight = new Vector3(localForward.Z, 0, -localForward.X);
            var localLeft = new Vector3(-localForward.Z, 0, localForward.X);

            if (gameInstance.Input.IsActive(Input.PlayerAction.Forward)) Transform.Position += localForward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Backward)) Transform.Position += -localForward * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Left)) Transform.Position += localRight * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameInstance.Input.IsActive(Input.PlayerAction.Right)) Transform.Position += localLeft * (float)gameTime.ElapsedGameTime.TotalSeconds;

            View = Matrix.CreateLookAt(Transform.Position, Transform.Position + GetLookAtVector(), Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane);

            localRight.Normalize();
            localLeft.Normalize();

            var point = new Vector2(Transform.Position.X, Transform.Position.Z) + new Vector2(localForwardNormalized.X, localForwardNormalized.Z);

            var offset = new Vector2(1f, 0);
            var start = point + new Vector2(localLeft.X, localLeft.Z);
            var end = point + new Vector2(localRight.X, localRight.Z);
            var line = new Line(start, end);
            var length = (end - start).Length();

            var height = Transform.Position.Y - 0.25f;

            MeshRenderer.Verticies = new VertexPositionTexture[]
            {
                new(new Vector3(line.Start.X, height, line.Start.Y) + localForwardNormalized, new Vector2(1, 1)),
                new(new Vector3(line.Start.X, height + 0.25f, line.Start.Y) + localForwardNormalized * (length + 1f), new Vector2(1, 0)),
                new(new Vector3(line.End.X, height + 0.25f, line.End.Y) + localForwardNormalized * (length + 1f), new Vector2(0, 0)),
                new(new Vector3(line.End.X, height, line.End.Y) + localForwardNormalized, new Vector2(0, 1)),
            };

            base.Update(gameTime);
        }

        private Vector3 GetLookAtVector()
        {
            var angle = Transform.Rotation;
            return new Vector3(MathF.Cos(angle * DegToRad), 0, MathF.Sin(angle * DegToRad));
        }
    }
}
