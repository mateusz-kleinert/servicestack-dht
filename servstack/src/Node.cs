using System;
using System.Collections.Generic;

namespace servstack
{
	public class Node
	{
		public Range primaryRange;
		public Range secondaryRange;
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

			Console.WriteLine("( " + primaryRange + ", " + secondaryRange + " )");
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

		public bool Insert (KeyValuePair<String, String> kv, int hn)
		{
			if (hn > 0 && hn < 3) {
				// compute hn
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

		public KeyValuePair<String, String> Find (String key, int hn)
		{
			if (hn > 0 && hn < 3) {
				// compute hn
				// route it
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

		public bool Delete (String Key, int hn)
		{
			if (hn > 0 && hn < 3) {
				// compute hn
				// route it
				return true;
			}
			return false;
		}

		public Tuple<Range, Range, List<Tuple<String, String>>> ChildCreate(String host)
		{
			var primarySplit = primaryRange.Split();
			var secondarySplit = secondaryRange.Split();
			primaryRange = primarySplit.Item1;
			secondaryRange = secondarySplit.Item1;

			childrens.Add(new Tuple<String, Range, Range> (host,primarySplit.Item2,secondarySplit.Item2));

			var chunk1 = hashTable.GetFromRange(primarySplit.Item2);
			var chunk2 = hashTable.GetFromRange(secondarySplit.Item2);
			var all = new List<Tuple<String, String>> (chunk1.Count + chunk2.Count);
			all.AddRange(chunk1);
			all.AddRange(chunk2);

			Console.WriteLine("( " + primaryRange + ", " + secondaryRange + " )");

			return new Tuple<Range, Range, List<Tuple<String, String>>> (
				primarySplit.Item2,
				secondarySplit.Item2,
				all
				);
		}
	}
}

