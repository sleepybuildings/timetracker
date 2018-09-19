using System;
using CommandLine;
using System.Collections.Generic;
using Timetracker.Tracker;

namespace Timetracker.Commands
{

	[VerbAttribute(
		"pop",
		HelpText = "Ends the current job and continues the previous one"
	)]
	public class PopJobsCommand : TrackerCommand
	{
		public override int RunTrackerCommand()
		{
			(Job poppedJob, Job startedJob) = Tracker.Pop();

			if(poppedJob == null && startedJob == null)
			{
				Console.WriteLine("There was no active job to pop!");
				return 1;
			}

			if(poppedJob != null)
				Console.WriteLine("Stopped active job {0}", poppedJob.Name);
			
			if(startedJob != null)
				Console.WriteLine("Job {0} resumed", startedJob.Name);

			return 0;
		}
	}
}
