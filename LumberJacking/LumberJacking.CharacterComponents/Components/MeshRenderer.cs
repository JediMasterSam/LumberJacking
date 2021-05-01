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
    public class MeshRenderer : Component
    {
        public MeshRenderer(BaseGameObject gameObject) : base(gameObject)
        {
        }

        public Model Model { get; set; }

        public override void UpdateComponent(GameTime gameTime)
        {
            return;
        }
    }
}
