using System;
using CommandLine;
using System.Collections.Generic;


namespace Timetracker.src.Commands
{

	[VerbAttribute(
		"list",
		HelpText = "Summary of todays jobs"
	)]
	public class ListJobsCommand: TrackerCommand
	{
		public override int RunTrackerCommand()
		{
			Console.WriteLine(" Timespan     Job");
			Console.WriteLine(new String((char)0x2550, 70));

			TimeSpan total = new TimeSpan();

			Tracker.jobs.ForEach((job) =>
			{
				var duration = job.GetDuration();
				total += duration;
				Console.WriteLine("{0,2} u {1,2} m     {2}",
				                  duration.Hours,
				                  duration.Minutes,
				                  job.Name + (job.IsActive? " (Still active)" : "")
				                 );
			});

			Console.WriteLine(new String((char)0x2550, 70));
			Console.WriteLine("{0,2} u {1,2} m", total.Hours,total.Minutes);

			return 0;
		}
	}
}
