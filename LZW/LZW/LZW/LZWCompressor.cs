
using System.Collections.Generic;

namespace LZWAlgorithms
{
    class LZWCompressor
    {
        public Dictionary<int, string> Compressor(string text, ref List<int> indices)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 866; i < 1143; i++)
            {
                dictionary.Add(i, new string((char)i, 1));
            }
            for (int j = 0; j < 128; j++)
            {
                dictionary.Add(j, new string((char)j, 1));
            }
            char c = ' ';
            int index = 1, n = text.Length, nextKey = 1143;
            string s = new string(text[0], 1), sc = string.Empty;
            while (index < n)
            {
                c = text[index++];
                sc = s + c;
                if (dictionary.ContainsValue(sc))
                {
                    s = sc;
                }
                else
                {
                    foreach (KeyValuePair<int, string> kvp in dictionary)
                    {
                        if (kvp.Value == s)
                        {
                            indices.Add(kvp.Key);
                            break;
                        }
                    }
                    dictionary.Add(nextKey++, sc);
                    s = new string(c, 1);
                }
            }
            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                if (kvp.Value == s)
                {
                    indices.Add(kvp.Key);
                    break;
                }
            }
            return dictionary;
        }
    }
}