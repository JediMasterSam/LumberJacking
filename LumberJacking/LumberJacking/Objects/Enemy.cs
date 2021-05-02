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
        public Enemy(Vector2 point, Texture2D texture2D, float height) : base(LumberJackingGame.Instance)
        {
            var (x, y) = point;
            Transform.Position = new Vector3(x, 0, y);
            MeshRenderer.Texture = texture2D;

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
            BlendState = BlendState.AlphaBlend;

            Circle = new Circle(point, 1);
        }
        
        private Circle Circle { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (Circle.Intersects(LumberJackingGame.Instance.Camera.GetComponent<BasicPhysics>().Circle))
            {
                Delete = true;
            }

            base.Update(gameTime);
        }
    }
}