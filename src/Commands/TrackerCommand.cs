using System;
using Timetracker.Tracker;
using Jobs = Timetracker.Tracker.Tracker;

namespace Timetracker.src.Commands
{
	abstract public class TrackerCommand: Command
	{
		
		virtual protected DateTime? dateTime { get; set; }

		public DateTime DateTime
		{
			get 
			{
				if(!dateTime.HasValue)
					dateTime = DateTime.Now;

				return dateTime.Value;
			}
		}


		protected Jobs tracker;

		protected Jobs Tracker 
		{
			get 
			{
				if(tracker == null)
					tracker = new Store().BuildTracker(DateTime);

				return tracker;
			}	
		}


		public override int Run()
		{
			try
			{
				return RunTrackerCommand();
					
			} finally 
			{
				StoreTracker();
			}
		}


		protected void StoreTracker()
		{
			if(tracker == null)
				return;

			new Store().StoreTracker(tracker);
		}


		abstract public int RunTrackerCommand();
	}
}
