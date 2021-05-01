using LumberJacking.GameObject.Interfaces;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject.Components
{
    public class BasicPhysics : Component
    {
        public BasicPhysics() : base(null)
        {
        }

        public BasicPhysics(BaseGameObject gameObject) : base(gameObject)
        {
            Circle = new Circle(new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Z), 0.25f);
            PreviousPosition = gameObject.Transform.Position;
        }

        public Circle Circle { get; set; }
        private Vector3 PreviousPosition { get; set; }

        public override void UpdateComponent(GameTime gameTime)
        {
            Circle.Center = new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Z);

            if (LumberJackingGame.Instance.Level.Walls.Any(wall => Circle.Intersects(wall)))
            {
                GameObject.Transform.Position = PreviousPosition;
            }

            PreviousPosition = GameObject.Transform.Position;
        }
    }
}
