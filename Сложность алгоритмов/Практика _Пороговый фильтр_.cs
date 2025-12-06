using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var allPixels = new List<double>();
		var result = new double[width, height];

        var threshold = GetThreshold(allPixels, original, whitePixelsFraction, width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = original[x, y] >= threshold ? 1.0 : 0.0;
            }
        }

        return result;
	}

	public static double GetThreshold(List<double> allPixels, 
									  double[,] original,
									  double whitePixelsFraction,
									  int width, int height)
	{
		for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allPixels.Add(original[x, y]);
            }
        }

		var whiteCount = (int)(whitePixelsFraction * width * height);

        if (whiteCount == 0)
            return double.MaxValue;

        if (whiteCount >= allPixels.Count)
            return double.MinValue;

        allPixels.Sort();

        return allPixels[allPixels.Count - whiteCount];
    }
}
