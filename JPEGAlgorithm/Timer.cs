using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    class Timer {
		private static Timer instance = null;

		private Dictionary<string, List<Interval>> intervals;

		private Timer() {
			intervals = new Dictionary<string, List<Interval>>();
		}

		public static Timer GetInstance() {
			if (instance != null) {
				return instance;
			}
			return new Timer();
		}

		private class Interval {
			public string title;

			public DateTime start;

			public DateTime end;

			public Interval(string title, DateTime start) {
				this.title = title;
				this.start = start;
			}
		}

		public void Start(string id, string title) {
			if (!intervals.ContainsKey(id)) {
				intervals.Add(id, new List<Interval> {new Interval(title, DateTime.Now)});
			} else {
				if (intervals[id].Exists(e => e.title.Equals(title))) {
					throw new ArgumentException("Can't start Interval with title = [" + title + "] it already exists");
				} else {
					intervals[id].Add(new Interval(title, DateTime.Now));
				}
			}
		}

		public void End(string id, string title) {
			if (intervals.ContainsKey(id) && intervals[id].Exists(e => e.title.Equals(title))) {
				intervals[id].Find(e => e.title.Equals(title)).end = DateTime.Now;
			} else {
				throw new ArgumentException("There is no such Interval to end for id = [" + id + "] and title = [" + title + "]");
			}
		}

		public void DisplayIntervals() {
			var sb = new StringBuilder();
			TimeSpan ts;
			foreach(var id in this.intervals.Keys) {
				sb.Append("Intervals with id = ").Append(id).AppendLine();
				foreach (var interval in this.intervals[id]) {
					ts = interval.end - interval.start;
					sb.Append("  ").Append(interval.title).AppendLine();
					sb.Append("  ").Append("Duration = ")
						.Append(ts.Seconds)
						.Append(" sec ")
						.Append(ts.Milliseconds)
						.Append(" ms").AppendLine();
				}
			}
			Console.WriteLine(sb.ToString());
		}
    }
}
