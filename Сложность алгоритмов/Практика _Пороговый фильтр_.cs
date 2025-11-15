using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var allPixels = new List<double>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allPixels.Add(original[x, y]);
            }
        }

        var whiteCount = (int)(whitePixelsFraction * width * height);
        var result = new double[width, height];
        var threshold = GetThreshold(allPixels, whiteCount);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = original[x, y] >= threshold ? 1.0 : 0.0;
            }
        }

        return result;
	}

    public static double GetThreshold(List<double> allPixels, int whiteCount)
    {
        if (whiteCount == 0)
            return double.MaxValue;

        if (whiteCount >= allPixels.Count)
            return double.MinValue;

        allPixels.Sort();

        return allPixels[allPixels.Count - whiteCount];
    }
}