using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
	public static double[,] MedianFilter(double[,] original)
	{
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var result = new double[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = GetMedian(original, x, y);
            }
        }

        return result;
    }

    public static double GetMedian(double[,] original, int x, int y)
    {
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var values = new List<double>();

        for (int rx = -1; rx <= 1; rx++)
        {
            for (int ry = -1; ry <= 1; ry++)
            {
                var fx = x + rx;
                var fy = y + ry;

                if (fx >= 0 && fx < width && fy >= 0 && fy < height)
                    values.Add(original[fx, fy]);
            }
        }

        values.Sort();

        if (values.Count % 2 == 1)
            return values[values.Count / 2];
        else
            return (values[values.Count / 2 - 1] + values[values.Count / 2]) / 2.0;
    }
}
