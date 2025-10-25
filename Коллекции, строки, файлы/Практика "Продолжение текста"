namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var words = phraseBeginning.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        for (int i = 0; i < wordsCount; i++)
        {
            var nextWord = "";

            if (words.Count >= 2 && nextWords.ContainsKey(words[^2] + " " + words[^1]))
                nextWord = nextWords[(words[^2] + " " + words[^1])];

            else if (nextWords.ContainsKey(words[^1]))
                nextWord = nextWords[words[^1]];

            if (nextWord == "")
                break;

            words.Add(nextWord);
        }
        return string.Join(" ", words);
    }
}
