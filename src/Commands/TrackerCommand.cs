using System;
using Timetracker.Tracker;
using Jobs = Timetracker.Tracker.Tracker;
using Timetracker.Contracts;
using Colorful;
using System.Drawing;

namespace Timetracker.Commands
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


		public int Run()
		{
			try
			{
				return RunTrackerCommand();

			} catch(Exception ex)
			{
				Colorful.Console.WriteLine("Oh no! Its an exception: " + ex.Message, Color.Red);
				return 1;

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
