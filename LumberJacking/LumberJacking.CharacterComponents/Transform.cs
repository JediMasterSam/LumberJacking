using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject
{
    public class Transform : IComponent
    {
        public Transform()
        {
            Position = new Vector3(0f, 0f, 0f);
            Rotation = 0f;
            Scale = new Vector3(0f, 0f, 0f);
        }

        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public Type Type => typeof(Transform);

        public void UpdateComponent(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
