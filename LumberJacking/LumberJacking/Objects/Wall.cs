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
        public Wall(Line line, float height = 10f) : base(LumberJackingGame.Instance)
        {
            Line = line;

            var vertices = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(line.Start.X, 0f, line.Start.Y), Color.AntiqueWhite),
                new VertexPositionColor(new Vector3(line.Start.X, height, line.Start.Y), Color.AntiqueWhite),
                new VertexPositionColor(new Vector3(line.End.X, height, line.End.Y), Color.AntiqueWhite),
                new VertexPositionColor(new Vector3(line.End.X, 0f, line.End.Y), Color.AntiqueWhite),
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
