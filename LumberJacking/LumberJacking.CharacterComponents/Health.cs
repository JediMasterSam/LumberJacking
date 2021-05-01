using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace LumberJacking.GameObject
{
    public class Health : IComponent
    {
        public Health(int max)
        {
            Points = MaxPoints = max;
        }

        public int Points { get; private set; }
        private int MaxPoints { get; set; }

        public Type Type => typeof(Health);

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

        public void UpdateComponent(GameTime gameTime)
        {
            return;
        }
    }
}
