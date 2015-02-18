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

		public bool Insert (KeyValuePair<String, String> kv)
		{
			if (kv.Key != "") {
				// compute h1
				// route it
				// compute h2
				// route it
				return true;
			}
			return false;
		}

		public KeyValuePair<String, String> Find (String key)
		{
			if (key != "") {
				// compute h1
				// compute h2
				// if both local then check em
				// if one local check him and route another if needed
				// if noone local route h1 and if not found, route h2
			}
			return new KeyValuePair<String, String> ("","");
		}

		public bool Delete (String Key)
		{
			if (Key != "") {
				// compute h1
				// route it
				// compute h2
				// route it
				return true;
			}
			return false;
		}
	}
}

