using System;
using System.Collections.Generic;

namespace servstack
{
	public class HashTable
	{
		public Dictionary<String, String> hashTable;

		// get, set

		public HashTable (List<Tuple<String, String>> data)
		{
			hashTable = new Dictionary<String, String>();

			foreach (Tuple<String, String> element in data) {
				hashTable.Add (element.Item1, element.Item2);
			}
		}

		public void Insert (KeyValuePair<String, String> kv)
		{
			//TODO:
		}

		public KeyValuePair<String, String> Find (String key)
		{
			//TODO:
			return new KeyValuePair<String, String> ("","");
		}

		public void Delete (String Key)
		{
			//TODO:
		}
	}
}

