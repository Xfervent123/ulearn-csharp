using System.Xml.Linq;

namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var birthsCounts = new double[30, 12];
        var days = new string[30];
        var months = new string[12];

        for (var i = 0; i < 30; i++)
            days[i] = (i + 2).ToString();

        for (var i = 0; i < 12; i++)
            months[i] = (i + 1).ToString();

        foreach (var person in names)
            if (person.BirthDate.Day != 1)
                birthsCounts[person.BirthDate.Day - 2, person.BirthDate.Month - 1]++;

        return new HeatmapData(
            "Пример карты интенсивностей",
            birthsCounts, 
            days, 
            months);
    }
}
