using System;

namespace servstack
{
	public class Range {
		public Range(int lb, int ub) 
		{
			lowerBound = lb;
			upperBound = ub;
		}

		public int lowerBound { get; set; }
		public int upperBound { get; set; }

		public override string ToString ()
		{
			return string.Format ("{0}x{1}", lowerBound, upperBound);
		}
	}
}

