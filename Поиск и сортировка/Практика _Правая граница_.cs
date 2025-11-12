using System;
using System.Collections.Generic;

namespace Autocomplete;

public class RightBorderTask
{
	public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
	{
        while (right - left > 1)
        {
            var middle = (right + left) / 2;
            if (phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) ||
                string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase) < 0)
            {
                left = middle;
            }
            else
            {
                right = middle;
            }
        }
        return right;
    }
}