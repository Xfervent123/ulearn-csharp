namespace Geometry
{
    public class Geometry()
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
    }

    public class Vector()
    {
        public double X;
        public double Y;
    }
}
