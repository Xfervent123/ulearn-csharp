using System;

namespace Geometry
{
    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            var X = vector.X;
            var Y = vector.Y;

            return Math.Abs(Math.Sqrt(X * X + Y * Y));
        }

        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            var X1 = firstVector.X;
            var Y1 = firstVector.Y;

            var X2 = secondVector.X;
            var Y2 = secondVector.Y;

            var vector = new Vector();
            vector.X = X1 + X2;
            vector.Y = Y1 + Y2;

            return vector;
        }

        public static double GetLength(Segment segment)
        {
            var firstCatheter = segment.End.X - segment.Begin.X;
            var secondCatheter = segment.End.Y - segment.Begin.Y;

            return Math.Sqrt(firstCatheter * firstCatheter + secondCatheter * secondCatheter);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var part1 = new Segment();
            part1.Begin = segment.Begin;
            part1.End = vector;

            var part2 = new Segment();
            part2.Begin = vector;
            part2.End = segment.End;

            var part = GetLength(part1) + GetLength(part2);
            return part == GetLength(segment);
        }
    }

    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector other)
        {
            return Geometry.Add(this, other);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}
