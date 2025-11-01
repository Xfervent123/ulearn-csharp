using System;

namespace Rectangles;

public static class RectanglesTask
{
	public static bool AreIntersected(Rectangle r1, Rectangle r2)
	{
		return IsOverlappingByAxis(r1.Left, r1.Right, r2.Left, r2.Right)
			&& IsOverlappingByAxis(r1.Top, r1.Bottom, r2.Top, r2.Bottom);
	}

	public static bool IsOverlappingByAxis(int start1, int end1, int start2, int end2)
	{
		return start1 <= end2 && start2 <= end1;
	}

    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
	{	
		if (!AreIntersected(r1, r2))
			return 0;
        var intersectLeft = Math.Max(r1.Left, r2.Left);
        var intersectTop = Math.Max(r1.Top, r2.Top);
        var intersectRight = Math.Min(r1.Right, r2.Right);
        var intersectBottom = Math.Min(r1.Bottom, r2.Bottom);
        var intersectWidth = intersectRight - intersectLeft;
        var intersectHeight = intersectBottom - intersectTop;
        return intersectWidth * intersectHeight;
    }

	public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
	{
		if (IsInsideByAxis(r1.Left, r1.Right, r2.Left, r2.Right)
			&& IsInsideByAxis(r1.Top, r1.Bottom, r2.Top, r2.Bottom))
			return 0;

		if (IsInsideByAxis(r2.Left, r2.Right, r1.Left, r1.Right) 
			&& IsInsideByAxis(r2.Top, r2.Bottom, r1.Top, r1.Bottom))
			return 1;
		return -1;
	}

	public static bool IsInsideByAxis(int start1, int end1, int start2, int end2)
	{
		return start1 >= start2 && end1 <= end2;
	}
}
