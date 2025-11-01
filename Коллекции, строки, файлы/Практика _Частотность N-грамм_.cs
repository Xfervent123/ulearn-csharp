namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var ngramsCount = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                if (sentence.Count < 2) continue;

                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    var start = sentence[i];
                    var next = sentence[i + 1];
                    AddGram(ngramsCount, start, next);
                }

                for (int i = 0; i < sentence.Count - 2; i++)
                {
                    var start = sentence[i] + " " + sentence[i + 1];
                    var next = sentence[i + 2];
                    AddGram(ngramsCount, start, next);
                }
            }

            SelectBestNextWords(ngramsCount, result);

            return result;
        }

        public static void AddGram(Dictionary<string, Dictionary<string, int>> dict, string start, string next)
        {
            if (!dict.ContainsKey(start))
                dict[start] = new Dictionary<string, int>();

            if (!dict[start].ContainsKey(next))
                dict[start][next] = 0;

            dict[start][next]++;
        }

		public static void SelectBestNextWords(Dictionary<string, 
Dictionary<string, int>> ngramsCount, Dictionary<string, string> result)
        {
            foreach (var item in ngramsCount)
            {
                var maxCount = 0;
                var bestNextWord = "";

                foreach (var continuation in item.Value)
                {
                    var nextWord = continuation.Key;
                    var count = continuation.Value;

                    if (count > maxCount)
                    {
                        maxCount = count;
                        bestNextWord = nextWord;
                    }

                    else if (count == maxCount &&
                             string.CompareOrdinal(nextWord, bestNextWord) < 0)
                    {
                        bestNextWord = nextWord;
                    }
                }

                result[item.Key] = bestNextWord;
            }
        }
    }
}
