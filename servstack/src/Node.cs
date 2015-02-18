using System;
using System.Collections.Generic;
using server.Services;
using ServiceStack.ServiceClient.Web;

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
				Insert (kv,1);
				Insert (kv,2);
				return true;
			}
			return false;
		}

		public bool Insert (KeyValuePair<String, String> kv, int hn)
		{
			if (hn > 0 && hn < 3 && kv.Key != "") {
				if (hn == 1) {
					int hash = HashFunctions.h1 (kv.Key);
					Console.WriteLine("H1: {0}",hash);
					String route = Route(hash,hn);
					if (route == "") {
						hashTable.Insert(kv);
					} else {
						try {
							var client = new JsonServiceClient ("http://" + route);
							client.Post(new Replica { Hn = hn, Key = kv.Key, Value = kv.Value });
						} catch (Exception) {
							Join (route);
						}
					}
				} else {
					int hash = HashFunctions.h2 (kv.Key);
					Console.WriteLine("H2: {0}",hash);
					String route = Route(hash,hn);
					if (route == "") {
						hashTable.Insert(kv);
					} else {
						try {
							var client = new JsonServiceClient ("http://" + route);
							client.Post(new Replica { Hn = hn, Key = kv.Key, Value = kv.Value });
						} catch (Exception) {
							Join (route);
						}
					}
				}
				return true;
			}
			return false;
		}

		public KeyValuePair<String, String> Find (String key)
		{
			if (key != "") {
				int hash1 = HashFunctions.h1 (key);
				int hash2 = HashFunctions.h2 (key);

				String route1 = Route(hash1,1);
				String route2 = Route(hash2,2);

				if (route1 == "") {
					var element = hashTable.Find(key);
					if (element.Key != "") {
						return element;
					}
				}
				if (route2 == "") {
					var element = hashTable.Find(key);
					if (element.Key != "") {
						return element;
					}
				}
				var found = Find (key,1);
				if (found.Key == "") {
					found = Find (key,2);
				}
				return found;
			}
			return new KeyValuePair<String, String> ("","");
		}

		public KeyValuePair<String, String> Find (String key, int hn)
		{
			if (hn > 0 && hn < 3 && key != "") {
				if (hn == 1) {
					int hash = HashFunctions.h1 (key);
					String route = Route(hash,hn);
					if (route == "") {
						return hashTable.Find(key);
					} else {
						try {
							var client = new JsonServiceClient ("http://" + route);
							ReplicaResponse resp = client.Get(new Replica { Hn = hn, Key = key, Value = "" });
							return resp.Result;
						} catch (Exception) {
							Join (route);
						}
					}
				} else {
					int hash = HashFunctions.h2 (key);
					String route = Route(hash,hn);
					if (route == "") {
						return hashTable.Find(key);
					} else {
						try {
							var client = new JsonServiceClient ("http://" + route);
							ReplicaResponse resp = client.Get(new Replica { Hn = hn, Key = key, Value = "" });
							return resp.Result;
						} catch (Exception) {
							Join (route);
						}
					}
				}
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
						try {
							var client = new JsonServiceClient ("http://" + route);
							client.Delete(new Replica { Hn = hn, Key = key, Value = "" });
						} catch (Exception) {
							Join (route);
						}
					}
				} else {
					int hash = HashFunctions.h2 (key);
					String route = Route(hash,hn);
					if (route == "") {
						hashTable.Delete(key);
					} else {
						try {
							var client = new JsonServiceClient ("http://" + route);
							client.Delete(new Replica { Hn = hn, Key = key, Value = "" });
						} catch (Exception) {
							Join (route);
						}
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

			int i;
			for (i = 0; i < childrens.Count; ++i) {
				if (childrens [i].Item1 == "") {
					primarySplit = new Tuple<Range, Range>(new Range(1, 1), childrens [i].Item2);
					secondarySplit = new Tuple<Range, Range>(new Range(1, 1), childrens [i].Item3);
					break;
				}
			}

			if (i < childrens.Count) {
				childrens.RemoveAt (i);
			} else {
				primaryRange = primarySplit.Item1;
				secondaryRange = secondarySplit.Item1;
			}

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

		public void Join (String host)
		{
			if (host != parent) {
				int i = 0;
				for (; i < childrens.Count; i++) {
					if (childrens [i].Item1 == host) {
						break;
					}
				}
				childrens.Add (new Tuple<String, Range, Range> ("", childrens[i].Item2, childrens[i].Item3));
				childrens.RemoveAt(i);
			} else {
				parent = "";
			}
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

