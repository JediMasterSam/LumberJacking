using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace LumberJacking.GameObject.Components
{
    public class Health : Component
    {
        public Health(BaseGameObject gameObject, int max) : base(gameObject)
        {
            Points = MaxPoints = max;
        }

        public int Points { get; private set; }
        private int MaxPoints { get; set; }

        public void Heal(int points)
        {
            Points = Math.Min(MaxPoints, Points + points);
        }

        public void Damage(int points)
        {
            Points = Math.Max(0, Points - points);
        }

        public void UpdateComponent()
        {
            throw new NotImplementedException();
        }

        public override void UpdateComponent(GameTime gameTime)
        {
            return;
        }
    }
}
