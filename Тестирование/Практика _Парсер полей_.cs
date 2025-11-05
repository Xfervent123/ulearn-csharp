using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
	public static void Test(string input, string[] expectedResult)
	{
		var actualResult = FieldsParserTask.ParseLine(input);
		ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
		for (int i = 0; i < expectedResult.Length; ++i)
		{
			ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
		}
	}

    [TestCase("text", new[] { "text" })]
    [TestCase("hello world", new[] { "hello", "world" })]
    [TestCase("", new string[0])]
    [TestCase("a  b  c", new[] { "a", "b", "c" })]
    [TestCase("  brat  ", new[] { "brat" })]
    [TestCase(@"""abc""", new[] { "abc" })]
    [TestCase("'abc'", new[] { "abc" })]
    [TestCase(@"""""", new[] { "" })]
    [TestCase(@"""a b""", new[] { "a b" })]
    [TestCase(@"""a"" ""b""", new[] { "a", "b" })]
    [TestCase(@"""abc", new[] { "abc" })]
    [TestCase(@"""a 'b' c""", new[] { "a 'b' c" })]
    [TestCase(@"""a \""b""", new[] { @"a ""b" })]
    [TestCase(@"""\\""", new[] { @"\" })]
    [TestCase(@"a""b""", new[] { "a", "b" })]
    [TestCase(@"""a""b", new[] { "a", "b" })]
    [TestCase("'a \"b\" c'", new[] { "a \"b\" c" })]
    [TestCase("'dmitriy\\'s'", new[] { "dmitriy's" })]
    [TestCase(@"""aby dabi ", new[] { "aby dabi " })]

    public static void RunTests(string input, string[] expectedOutput)
    {
        Test(input, expectedOutput);
    }
}

public class FieldsParserTask
{
	public static List<Token> ParseLine(string line)
	{
        var tokens = new List<Token>();
        var index = 0;

        while (index < line.Length)
        {
            index = SkipSpaces(line, index);

            if (index >= line.Length) break;

            var token = ReadField(line, index);
            tokens.Add(token);
            index = token.GetIndexNextToToken();
        }
        return tokens;
    }

    public static int SkipSpaces(string line, int startIndex)
    {
        while (startIndex < line.Length && line[startIndex] == ' ')
            startIndex++;

        return startIndex;
    }

    private static Token ReadField(string line, int startIndex)
	{
        return line[startIndex] == '"' || line[startIndex] == '\''
            ? ReadQuotedField(line, startIndex) : ReadSimpleField(line, startIndex);
    }

    public static Token ReadSimpleField(string line, int startIndex)
    {
        var index = startIndex;

        while (index < line.Length && line[index] != ' '
            && line[index] != '"' && line[index] != '\'')
            index++;

        return new Token(line.Substring(startIndex, index - startIndex),
            startIndex, index - startIndex);
    }

    public static Token ReadQuotedField(string line, int startIndex)
	{
		return QuotedFieldTask.ReadQuotedField(line, startIndex);
	}
}
