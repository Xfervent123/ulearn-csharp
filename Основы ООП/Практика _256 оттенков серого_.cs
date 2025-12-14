using System.Collections.Generic;
using Avalonia.Media;
using Geometry;

public static class SegmentExtensions
{
    static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();

    public static void SetColor(this Segment segment, Color color)
    {
        if (colors.ContainsKey(segment))
            colors[segment] = color;
        else
            colors.Add(segment, color);
    }

    public static Color GetColor(this Segment segment)
    {
        if (colors.TryGetValue(segment, out var color))
            return color;

        return Color.FromRgb(0, 0, 0);
    }
}
