namespace Passwords;

public class CaseAlternatorTask
{
	public static List<string> AlternateCharCases(string lowercaseWord)
	{
		var result = new List<string>();
		AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
		return result;
	}

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (startIndex == word.Length)
        {
            result.Add(new string(word));
            return;
        }

        var ch = word[startIndex];
        if (!char.IsLetter(ch))
        {
            AlternateCharCases(word, startIndex + 1, result);
            return;
        }

        word[startIndex] = char.ToLower(ch);
        AlternateCharCases(word, startIndex + 1, result);

        var upperCh = char.ToUpper(ch);
        if (upperCh != word[startIndex])
        {
            word[startIndex] = upperCh;
            AlternateCharCases(word, startIndex + 1, result);
        }

        word[startIndex] = ch;
    }
}
