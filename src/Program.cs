using System;
using Timetracker.Tracker;

namespace Timetracker
{
	class Program
	{		

		static void Main(string[] args)
		{
			var tracker = new Store().BuildTracker(DateTime.Now);

			try
			{
				
				Job prev = null;

				prev = tracker.Push("job 1");
				Console.WriteLine("Starting 1, ending " + (prev == null ? "none" : prev.Name));

				prev = tracker.Push("job 2");
				Console.WriteLine("Starting 2, ending " + (prev == null ? "none" : prev.Name));

				tracker.End();


			} finally 
			{
				new Store().StoreTracker(tracker);
			}
		}
	}
}

