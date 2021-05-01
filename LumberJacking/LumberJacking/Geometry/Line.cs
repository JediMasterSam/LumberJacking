using System;
using Microsoft.Xna.Framework;

namespace LumberJacking.Geometry
{
    public sealed class Line
    {
        public Line(Vector2 start, Vector2 end)
        {
            var slope = new Vector2(start.Y - end.Y, end.X - start.X);
            slope.Normalize();

            var a = slope.X;
            var b = slope.Y;
            var c = a * start.X + b * start.Y;

            Start = start;
            End = end;

            A = a;
            B = b;
            C = c;
        }

        public Vector2 Start { get; }

        public Vector2 End { get; }

        private float A { get; }

        private float B { get; }

        private float C { get; }

        public bool Contains(Vector2 point)
        {
            return GetSign(Start) + GetSign(End) == Vector2.Zero && AreEqual(A * point.X + B * point.Y, C) || Start == point || End == point;

            Vector2 GetSign(Vector2 other)
            {
                var (x, y) = point - other;
                return new Vector2(Math.Sign(x), Math.Sign(y));
            }
        }

        public bool X(float y, out float x)
        {
            if (AreEqual(A, 0f))
            {
                x = float.NaN;
                return false;
            }

            var bY = B * y;

            x = !AreEqual(bY, 0f) ? (C - bY) / A : C / A;
            return true;
        }

        public float Y(float x)
        {
            if (AreEqual(B, 0f)) return float.NaN;

            var aX = A * x;

            return !AreEqual(aX, 0f) ? (C - aX) / B : C / B;
        }

        private static bool AreEqual(in float a, in float b)
        {
            return Math.Abs(a - b) < .001f;
        }
    }
}