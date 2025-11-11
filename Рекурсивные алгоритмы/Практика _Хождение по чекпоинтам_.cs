using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    static double bestLength;
    static int[] bestWay;

    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
	{
        bestLength = double.MaxValue;
        bestWay = new int[checkpoints.Length];

        var currentWay = new int[checkpoints.Length];
        var visited = new bool[checkpoints.Length];

        visited[0] = true;
        currentWay[0] = 0;

        RecursionSearch(checkpoints, visited, 1,  currentWay, 0.0);

        return bestWay;
    }

    static void RecursionSearch(Point[] checkpoints, bool[] visited, int position, 
                       int[] currentWay, double currentLength)
    {
        if (currentLength >= bestLength) return;

        if (position == checkpoints.Length)
        {
            bestLength = currentLength;
            Array.Copy(currentWay, bestWay, currentWay.Length);
            return;
        }

        var last = checkpoints[currentWay[position - 1]];

        for (int i = 1; i < checkpoints.Length; i++)
        {
            if (visited[i]) continue;

            var next = checkpoints[i];
            var newLength = currentLength + PointExtensions.DistanceTo(last, next);

            visited[i] = true;
            currentWay[position] = i;
            RecursionSearch(checkpoints, visited, position + 1, currentWay, newLength);
            visited[i] = false;
        }
    }
}
