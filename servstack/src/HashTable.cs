using System;
using System.Collections.Generic;
using ServiceStack.Text;

namespace servstack
{
	public class HashTable
	{
		public Dictionary<String, String> hashTable;

		public HashTable (List<Tuple<String, String>> data)
		{
			hashTable = new Dictionary<String, String>();

			foreach (Tuple<String, String> element in data) {
				hashTable.Add (element.Item1, element.Item2);
			}
		}

		public void Insert (KeyValuePair<String, String> kv)
		{
			if (!hashTable.ContainsKey (kv.Key)) {
				hashTable.Add (kv.Key, kv.Value);
				Console.WriteLine (hashTable.ToJson ());
			}
		}

		public KeyValuePair<String, String> Find (String key)
		{
			if (hashTable.ContainsKey(key)) {
				return new KeyValuePair<string, string> (key, hashTable[key]);
			}
			return new KeyValuePair<String, String> ("","");
		}

		public void Delete (String key)
		{
			hashTable.Remove(key);
		}

		public List<Tuple<String, String>> GetFromRange (Range range, int hn)
		{
			var result = new List<Tuple<String, String>> ();
			foreach (KeyValuePair<String, String> element in hashTable) {
				if ((hn == 1 && range.Contains (HashFunctions.h1 (element.Key))) ||
					(hn == 2 && range.Contains (HashFunctions.h2 (element.Key)))) {
					result.Add (new Tuple<String, String> (element.Key, element.Value));
				}
			}
			return new List<Tuple<String, String>> ();
		}
	}
}

