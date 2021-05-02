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
        public CustomMeshRenderer(BaseGameObject gameObject, GraphicsDevice device) : base(gameObject)
        {
            Effect = new AlphaTestEffect(device);
        }

        public AlphaTestEffect Effect { get; set; }
        public Texture2D Texture { get; set; }
        public VertexPositionTexture[] Verticies { get; set; }
        public short[] TriangleIndices { get; set; }

        public override void UpdateComponent(GameTime gameTime)
        {
            return;
        }
    }
}
