using Microsoft.Xna.Framework;

namespace LumberJacking.Geometry
{
    public sealed class Circle
    {
        public Circle(Vector2 center, float radius)
        {
            Center = center;
            RadiusSquared = radius * radius;
        }

        public Vector2 Center { get; set; }

        private float RadiusSquared { get; }

        public bool Contains(Vector2 point)
        {
            return (Center - point).LengthSquared() <= RadiusSquared;
        }

        public bool Intersects(Line line)
        {
            if (Contains(line.Start) || Contains(line.End)) return true;

            var point = line.X(Center.Y, out var x) ? new Vector2(x, Center.Y) : new Vector2(Center.X, line.Y(Center.X));

            return Contains(point) && line.Contains(point);
        }
    }
}