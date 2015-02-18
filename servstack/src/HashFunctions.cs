using System;

namespace servstack
{
	public class HashFunctions
	{
		public static int h1 (String key)
		{
			var hash = Math.Abs(key.GetHashCode() * 1327);
			//var hash = key.Length * 1327;
			return hash % (Hardcode.PrimaryUpperBound - Hardcode.PrimaryLowerBound + 1) + Hardcode.PrimaryLowerBound;
		}

		public static int h2 (String key)
		{
			//var hash = key.GetHashCode() * 7919;
			var hash = Math.Abs(key.Length * 7919);
			return hash % (Hardcode.SecondaryUpperBound - Hardcode.SecondaryLowerBound + 1) + Hardcode.SecondaryLowerBound;
		}
	}
}

