using System;

namespace servstack
{
	public class HashFunctions
	{
		public static int h1 (String key)
		{
			//var hash = key.GetHashCode () > 0 ? key.GetHashCode () : -key.GetHashCode ();
			var hash = key.Length * 1327;
			return hash % (Hardcode.PrimaryUpperBound - Hardcode.PrimaryLowerBound + 1) + Hardcode.PrimaryLowerBound;
		}

		public static int h2 (String key)
		{
			//var hash = key.GetHashCode () > 0 ? key.GetHashCode () : -key.GetHashCode ();
			var hash = key.Length * 3251;
			return hash % (Hardcode.SecondaryUpperBound - Hardcode.SecondaryLowerBound + 1) + Hardcode.SecondaryLowerBound;
		}
	}
}

