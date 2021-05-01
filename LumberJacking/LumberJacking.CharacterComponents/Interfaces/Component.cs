using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject.Interfaces
{
    public abstract class Component
    {
        public Component(BaseGameObject gameObject)
        {
            GameObject = gameObject;
        }

        public BaseGameObject GameObject { get; set; }
        public abstract void UpdateComponent(GameTime gameTime);
    }
}
