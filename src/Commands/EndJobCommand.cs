using System;
using CommandLine;

namespace Timetracker.Commands
{

	[VerbAttribute(
		"end",
		HelpText = "Ends the current job"
	)]
	public class EndJobCommand: TrackerCommand
	{
		
		public override int RunTrackerCommand()
		{
			var endedJob = Tracker.End();

			if(endedJob == null)
			{
				Console.WriteLine("No jobs where active");
				return 1;
			}

			Console.WriteLine(string.Format("Job {0} stopped.", endedJob.Name));
			return 0;
		}


	}
}
