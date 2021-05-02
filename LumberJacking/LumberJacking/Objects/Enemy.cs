using System;
using LumberJacking.GameObject;
using LumberJacking.GameObject.Components;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumberJacking.Objects
{
    public class Enemy : BaseGameObject
    {
        private const float DegToRad = 180f / MathF.PI * 2;
        private const float RadToDeg = MathF.PI * 2 / 180;


        public Enemy(Vector2 point, Texture2D texture2D, float height) : base(LumberJackingGame.Instance)
        {
            var (x, y) = point;
            Transform.Position = new Vector3(x, 0, y);
            MeshRenderer.Texture = texture2D;
            MeshRenderer.Effect = new AlphaTestEffect(GraphicsDevice);

            var offset = new Vector2(.5f, 0);
            var start = point - offset;
            var end = point + offset;
            var line = new Line(start, end);
            var length = (end - start).Length();


            var vertices = new VertexPositionTexture[]
            {
                new(new Vector3(line.Start.X, 0f, line.Start.Y), new Vector2(0, height)),
                new(new Vector3(line.Start.X, height, line.Start.Y), new Vector2(0, 0)),
                new(new Vector3(line.End.X, height, line.End.Y), new Vector2(length, 0)),
                new(new Vector3(line.End.X, 0f, line.End.Y), new Vector2(length, height)),
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

            Circle = new Circle(point, 1);
        }

        private Circle Circle { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (Circle.Intersects(LumberJackingGame.Instance.Camera.GetComponent<BasicPhysics>().Circle))
            {
                Delete = true;
            }

            // var player = LumberJackingGame.Instance.Camera.Transform.Position;
            // var angle = Transform.Rotation;
            // var current = new Vector3(MathF.Cos(angle * DegToRad), 0, MathF.Sin(angle * DegToRad));
            // var lookAt = player - Transform.Position;
            //
            // current.Normalize();
            // lookAt.Normalize();
            //
            // angle = Vector3.Dot(current, lookAt);
            //
            // if(Math.Abs(angle - Transform.Rotation * DegToRad) < .1f)return;
            
            var cos = MathF.Cos(0.0174533f);
            var sin = MathF.Sin(0.0174533f);

            // Transform.Rotation = (angle * RadToDeg);

            for (var index = 0; index < MeshRenderer.Verticies.Length; index++)
            {
                var temp = MeshRenderer.Verticies[index];
                var p = temp.Position;
                var (x, _, z) = p - Transform.Position;
                var update = new Vector3(x * cos - z * sin, p.Y, z * cos + x * sin) + Transform.Position;

                MeshRenderer.Verticies[index] = new VertexPositionTexture(update, temp.TextureCoordinate);
            }

            base.Update(gameTime);
        }
    }
}