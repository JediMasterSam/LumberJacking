using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject.Interfaces
{
    public interface IComponent
    {
        public void UpdateComponent(GameTime gameTime);
        public Type Type { get; }
    }
}
