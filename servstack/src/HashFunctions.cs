using System;

namespace servstack
{
	public class HashFunctions
	{
		public static int Prime = 1327;
		public static int h1 (String key)
		{
			var hash = key.Length * Prime;
			return hash % (Hardcode.PrimaryUpperBound - Hardcode.PrimaryLowerBound + 1) + Hardcode.PrimaryLowerBound;
		}

		public static int h2 (String key)
		{
			var hash = key.Length * Prime;
			return hash % (Hardcode.SecondaryUpperBound - Hardcode.SecondaryLowerBound + 1) + Hardcode.SecondaryLowerBound;
		}
	}
}

