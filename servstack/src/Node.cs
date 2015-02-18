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
			if (hn > 0 && hn < 3 && kv.Key != "") {
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
			if (hn > 0 && hn < 3 && key != "") {
				// compute hn
				// route it
			}
			return new KeyValuePair<String, String> ("","");
		}

		public bool Delete (String key)
		{
			if (key != "") {
				Delete (key,1);
				Delete (key,2);
				return true;
			}
			return false;
		}

		public bool Delete (String key, int hn)
		{
			if (hn > 0 && hn < 3 && key != "") {
				if (hn == 1) {
					int hash = HashFunctions.h1 (key);
					String route = Route(hash,hn);
					if (route == "") {
						hashTable.Delete(key);
					} else {
						//
					}
				} else {
					int hash = HashFunctions.h2 (key);
					String route = Route(hash,hn);
					if (route == "") {
						hashTable.Delete(key);
					} else {
						//
					}
				}
				return true;
			}
			return false;
		}

		public Tuple<Range, Range, List<Tuple<String, String>>> ChildCreate (String host)
		{
			var primarySplit = primaryRange.Split ();
			var secondarySplit = secondaryRange.Split ();
			primaryRange = primarySplit.Item1;
			secondaryRange = secondarySplit.Item1;

			childrens.Add (new Tuple<String, Range, Range> (host, primarySplit.Item2, secondarySplit.Item2));

			var chunk1 = hashTable.GetFromRange (primarySplit.Item2, 1);
			var chunk2 = hashTable.GetFromRange (secondarySplit.Item2, 2);
			var all = new List<Tuple<String, String>> (chunk1.Count + chunk2.Count);
			all.AddRange (chunk1);
			all.AddRange (chunk2);

			foreach (Tuple<String, String> element in all) {
				if (!primaryRange.Contains (HashFunctions.h1 (element.Item1)) &&
					!primaryRange.Contains (HashFunctions.h2 (element.Item1)) &&
					!secondaryRange.Contains (HashFunctions.h1 (element.Item1)) &&
					!secondaryRange.Contains (HashFunctions.h2 (element.Item1))) {
					hashTable.hashTable.Remove(element.Item1);
				}
			}

			Console.WriteLine("( " + primaryRange + ", " + secondaryRange + " )");

			return new Tuple<Range, Range, List<Tuple<String, String>>> (
				primarySplit.Item2,
				secondarySplit.Item2,
				all
				);
		}

		public void ChildJoin (String host)
		{
			//TODO: take child over
		}

		public void ParentJoin (String host)
		{
			//TODO: take parent over
		}

		private String Route (int hash, int hn)
		{
			if (hn == 1) {
				if (primaryRange.Contains(hash)) {
					return "";
				}
				foreach (Tuple<String, Range, Range> child in childrens) {
					if (child.Item2.Contains(hash)) {
						return child.Item1;
					}
				}
			} else {
				if (secondaryRange.Contains(hash)) {
					return "";
				}
				foreach (Tuple<String, Range, Range> child in childrens) {
					if (child.Item3.Contains(hash)) {
						return child.Item1;
					}
				}
			}
			return parent;
		}
	}
}

