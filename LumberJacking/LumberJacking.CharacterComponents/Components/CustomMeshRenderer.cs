using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject.Components
{
    public class CustomMeshRenderer : Component
    {
        public CustomMeshRenderer() : base(null)
        {
        }

        public CustomMeshRenderer(BaseGameObject gameObject) : base(gameObject)
        {
        }

        public VertexPositionColor[] Verticies { get; set; }
        public short[] TriangleIndices { get; set; }

        public override void UpdateComponent(GameTime gameTime)
        {
            return;
        }
    }
}
