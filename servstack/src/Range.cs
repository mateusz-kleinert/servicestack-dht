using System;

namespace servstack
{
	public class Range {
		public int lowerBound { get; set; }
		public int upperBound { get; set; }

		public Range(int lb, int ub) 
		{
			lowerBound = lb;
			upperBound = ub;
		}

		public Tuple<Range, Range> Split() {
			return new Tuple<Range, Range> (new Range(lowerBound, (lowerBound+upperBound)/2),
			                            new Range((lowerBound+upperBound)/2 + 1, upperBound));
		}

		public override string ToString ()
		{
			return string.Format ("({0}x{1})", lowerBound, upperBound);
		}
	}
}

