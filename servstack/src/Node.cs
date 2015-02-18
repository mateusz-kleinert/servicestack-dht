using System;
using System.Collections.Generic;

namespace servstack
{
	public class Node
	{
		public Range primaryRange, secondaryRange;
		public String parent;
		public List<Tuple<String, Range, Range>> childrens;

		public HashTable hashTable;

		public Node (Range primaryRange, Range secondaryRange, String parent, List<Tuple<String, String>> data)
		{
			this.primaryRange = primaryRange;
			this.secondaryRange = secondaryRange;
			this.parent = parent;

			childrens = new List<Tuple<String, Range, Range>> ();
			hashTable = new HashTable (data);
		}

		public void Insert (KeyValuePair<String, String> kv)
		{
			//TODO: add/update & replicate
		}

		public KeyValuePair<String, String> Find (String key)
		{
			//TODO: lookup
		}

		public void Delete (String Key)
		{
			//TODO: delete & replicate(delete)
		}
	}
}

