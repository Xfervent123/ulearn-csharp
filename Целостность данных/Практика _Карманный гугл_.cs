using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
	Dictionary<string, Dictionary<int, List<int>>> Data = new Dictionary<string, Dictionary<int, List<int>>>();
    Dictionary<int, List<string>> WordsInDoc = new Dictionary<int, List<string>>();
    public void Add(int id, string documentText)
	{
		var splitDocument = documentText.Split(new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' });
		var index = 0;

        if (!WordsInDoc.ContainsKey(id))
        {
            WordsInDoc[id] = new List<string>();
        }

        foreach (var word in splitDocument)
        {
			if (word.Length > 0)
			{
				WordsInDoc[id].Add(word);

				if (!Data.ContainsKey(word))
					Data[word] = new Dictionary<int, List<int>>();

				if (!Data[word].ContainsKey(id))
					Data[word][id] = new List<int>();

				Data[word][id].Add(index);
			}

			index += word.Length + 1;
        }
    }

	public List<int> GetIds(string word)
	{
		if (!Data.ContainsKey(word)) return new List<int>();
		return Data[word].Keys.ToList();
	}

	public List<int> GetPositions(int id, string word)
	{
        if (!Data.ContainsKey(word) || !Data[word].ContainsKey(id)) return new List<int>();
        return Data[word][id];
    }

	public void Remove(int id)
	{
        if (!WordsInDoc.ContainsKey(id)) return;

        foreach (var word in WordsInDoc[id])
        {
            if (Data.ContainsKey(word) && Data[word].ContainsKey(id))
            {
                Data[word].Remove(id);
            }
        }

        WordsInDoc.Remove(id);
    }
}
