using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var rows = g.GetLength(0);
        var cols = g.GetLength(1);
        var kRows = sx.GetLength(0);
        var kCols = sx.GetLength(1);
        var kRowHalf = kRows / 2;
        var kColHalf = kCols / 2;

        var result = new double[rows, cols];

        for (int i = kRowHalf; i < rows - kRowHalf; i++)
        {
            for (int j = kColHalf; j < cols - kColHalf; j++)
            {
                var gx = 0.0;
                var gy = 0.0;

                for (int ki = 0; ki < kRows; ki++)
                {
                    for (int kj = 0; kj < kCols; kj++)
                    {
                        var neighborRow = i + ki - kRowHalf;
                        var neighborCol = j + kj - kColHalf;
                        gx += sx[ki, kj] * g[neighborRow, neighborCol];
                        gy += sx[kj, ki] * g[neighborRow, neighborCol];
                    }
                }

                result[i, j] = Math.Sqrt(gx * gx + gy * gy);
            }
        }
        return result;
    }
}
