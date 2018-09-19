using System;
using CommandLine;

namespace Timetracker.Commands
{

	[VerbAttribute(
		"start",
		HelpText = "Start or continue a job"
	)]
	public class StartJobCommand : TrackerCommand
	{

		[Value(1, HelpText = "Name of the job")]
		public string Name { get; set; }


		public override int RunTrackerCommand()
		{
			if(string.IsNullOrEmpty(Name))
			{
				Console.WriteLine("Name of the job cannot be empty");
				return 1;
			}

			var endedJob = Tracker.Push(Name);
			Console.WriteLine(string.Format("Job with name {0} has been started", Name));
			
			if(endedJob != null)
				Console.WriteLine(string.Format("Job {0} stopped.", endedJob.Name));

			return 0;
		}


	}
}
