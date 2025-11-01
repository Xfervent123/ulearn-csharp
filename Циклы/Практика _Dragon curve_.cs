using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        var random = new Random(seed);
        var x = 1.0;
        var y = 0.0;

        var sqrt2 = Math.Sqrt(2);
        var angle45 = Math.PI / 4;
        var angle135 = 3 * Math.PI / 4;

        pixels.SetPixel(x, y);

        for (int i = 0; i < iterationsCount; i++)
        {
            if (random.Next(2) == 0)
                (x, y) = CalculateParameters(x, y, angle45, sqrt2, 0);
            else
                (x, y) = CalculateParameters(x, y, angle135, sqrt2, 1);

            pixels.SetPixel(x, y);
        }
    }

    public static (double, double) CalculateParameters(double x, double y, double angle, double sqrt2, double shiftX)
    {
        var newX = (x * Math.Cos(angle) - y * Math.Sin(angle)) / sqrt2 + shiftX;
        var newY = (x * Math.Sin(angle) + y * Math.Cos(angle)) / sqrt2;
        return (newX, newY);
    }
}
