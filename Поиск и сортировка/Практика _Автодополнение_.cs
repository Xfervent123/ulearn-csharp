using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;

namespace Autocomplete;

internal class AutocompleteTask
{
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
        var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);

        var result = new List<string>();

        for (int i = leftIndex + 1; i < phrases.Count && result.Count < count; i++)
        {
            if (phrases[i].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                result.Add(phrases[i]);
            }
            else
            {
                break;
            }
        }

        return result.ToArray();
    }

	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
        var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        var count = rightIndex - leftIndex - 1;

        return count;
	}
}

[TestFixture]
public class AutocompleteTests
{
    // тесты для FindFirstByPrefix

    [Test]
    public void FirstFound()
    {
        var phrases = new List<string> { "aa", "ab", "abc" };
        var result = AutocompleteTask.FindFirstByPrefix(phrases, "ab");
        ClassicAssert.AreEqual("ab", result);
    }

    [Test]
    public void FirstNull()
    {
        var phrases = new List<string> { "a", "b" };
        var result = AutocompleteTask.FindFirstByPrefix(phrases, "w");
        ClassicAssert.IsNull(result);
    }

    // тесты для GetTopByPrefix

    [Test]
    public void TopFound()
    {
        var phrases = new List<string> { "ab", "abc", "abd", "dima" };
        var result = AutocompleteTask.GetTopByPrefix(phrases, "ab", 2);
        CollectionAssert.AreEqual(new[] { "ab", "abc" }, result);
    }

    [Test]
    public void TopEmpty()
    {
        var phrases = new List<string> { "a", "b" };
        var result = AutocompleteTask.GetTopByPrefix(phrases, "w", 5);
        CollectionAssert.IsEmpty(result);
    }

    // тесты для GetCountByPrefix

    [Test]
    public void CountCorrect()
    {
        var phrases = new List<string> { "a", "ab", "abc", "b" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "a");
        ClassicAssert.AreEqual(3, result);
    }

    [Test]
    public void CountZero()
    {
        var phrases = new List<string> { "a", "b" };
        var result = AutocompleteTask.GetCountByPrefix(phrases, "w");
        ClassicAssert.AreEqual(0, result);
    }
}
