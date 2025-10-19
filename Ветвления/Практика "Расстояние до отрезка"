using System;

namespace DistanceTask;

public static class DistanceTask
{
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
		if (bx - ax == 0 && by - ay == 0)
		{
            return GetDistance(x, y, ax, ay);
		}

		var projectionFactor = ((x - ax) * (bx - ax) + (y - ay) * (by - ay)) 
							/ ((bx - ax) * (bx - ax) + (by - ay) * (by - ay));

        if (projectionFactor < 0)
        {
            return GetDistance(x, y, ax, ay);
        }
        if (projectionFactor > 1)
        {
            return GetDistance(x, y, bx, by);
        }
        var qx = (ax + projectionFactor * (bx - ax));
        var qy = (ay + projectionFactor * (by - ay));
        return GetDistance(x, y, qx, qy);
	}

	public static double GetDistance(double x1, double y1, double x2, double y2)
	{
		return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}
}
