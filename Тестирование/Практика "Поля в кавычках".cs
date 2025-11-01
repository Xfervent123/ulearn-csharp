using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Reflection.Metadata.Ecma335;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
    [TestCase("\"abc", 0, "abc", 4)]
    [TestCase("'abcd", 0, "abcd", 5)]
    [TestCase("\"abc d f", 0, "abc d f", 8)]
    [TestCase("abc\"def\"", 3, "def", 5)]

    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
	{
		var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
	}
}

class QuotedFieldTask
{
	public static Token ReadQuotedField(string line, int startIndex)
	{
        var i = startIndex + 1;
        var value = "";

        while (i < line.Length && line[i] != line[startIndex])
        {
            if (line[i] == '\\' && i + 1 < line.Length)
            {
                value += line[i + 1];
                i += 2;
            }
            else
            {
                value += line[i];
                i++;
            }
        }

        var length = i - startIndex;
        if (i < line.Length)
            length++;

        return new Token(value, startIndex, length);
    }
}
