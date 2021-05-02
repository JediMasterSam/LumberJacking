using LumberJacking.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.Objects
{
    public class Floor : BaseGameObject
    {
        public Floor(float scale, Texture2D texture) : base(LumberJackingGame.Instance)
        {
            MeshRenderer.Texture = texture;

            var vertices = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1f, 0f, -1f) * scale, new Vector2(0, scale)),
                new VertexPositionTexture(new Vector3(-1f, 0f, 1f) * scale, new Vector2(0, 0)),
                new VertexPositionTexture(new Vector3(1f, 0f, 1f) * scale, new Vector2(scale, 0)),
                new VertexPositionTexture(new Vector3(1f, 0f, -1f) * scale, new Vector2(scale, scale)),
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
    }
}
