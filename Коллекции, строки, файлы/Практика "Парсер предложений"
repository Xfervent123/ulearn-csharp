using System.Text;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        char[] separators = { '!', '?', ';', ':', '(', ')', '.' };
        var sentences = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
		var sentencesList = new List<List<string>>();

        foreach (var sentence in sentences)
        {
            var words = ParseSentence(sentence);
            if (words.Count > 0)
                sentencesList.Add(words);
        }

        return sentencesList;
    }

    public static List<string> ParseSentence(string sentence)
    {
        var words = new List<string>();
        var word = new StringBuilder();

        foreach (var ch in sentence)
        {
            if (char.IsLetter(ch) || ch == '\'')
                word.Append(char.ToLower(ch));
            else if (word.Length > 0)
            {
                words.Add(word.ToString());
                word.Clear();
            }
        }

        if (word.Length > 0)
			words.Add(word.ToString());

        return words;
    }
}
