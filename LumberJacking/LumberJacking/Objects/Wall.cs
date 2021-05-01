using LumberJacking.GameObject;
using LumberJacking.GameObject.Components;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.Objects
{
    public class Wall : BaseGameObject
    {
        public Wall(Line line, Texture2D texture, float height = 10f) : base(LumberJackingGame.Instance)
        {
            Line = line;
            MeshRenderer.Texture = texture;

            var length = (line.Start - line.End).Length();

            var vertices = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(line.Start.X, 0f, line.Start.Y), new Vector2(0, height)),
                new VertexPositionTexture(new Vector3(line.Start.X, height, line.Start.Y), new Vector2(0, 0)),
                new VertexPositionTexture(new Vector3(line.End.X, height, line.End.Y), new Vector2(length, 0)),
                new VertexPositionTexture(new Vector3(line.End.X, 0f, line.End.Y), new Vector2(length, height)),
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

        public Line Line { get; set; }
    }
}
