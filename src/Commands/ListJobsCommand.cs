using System;
using CommandLine;
using System.Collections.Generic;
using Timetracker.Extensions;
using System.Linq;

namespace Timetracker.src.Commands
{

	[VerbAttribute(
		"list",
		HelpText = "Summary of todays jobs"
	)]
	public class ListJobsCommand : TrackerCommand
	{

		[Option('d', "date", HelpText = "Date to list")]
		public DateTime Date
		{
			get => dateTime.HasValue? dateTime.Value : DateTime.Now;
			set => dateTime = value;
		}

		[Value(0, HelpText = "Use 'prev' to show a summay of yesterday")]
		public string Selector { get; set; }


		public override int RunTrackerCommand()
		{
			// Check if the user wants to show the entries from yesterday

			if(Selector == "prev")
				Date = DateTime.Now.AddDays(-1);
			
			// Print the date

			if(Date.SameDay(DateTime.Now))
				Console.WriteLine(" Summary of today");
			else
				Console.WriteLine(" Summary of {0}-{1}-{2}",
				                  Date.Day.ToString().PadLeft(2, '0'),
								  Date.Month.ToString().PadLeft(2, '0'),
								  Date.Year
								 );

			// Summary table

			Console.WriteLine(" Timespan     Job");
			Console.WriteLine(new String((char)0x2550, 70));

			TimeSpan total = new TimeSpan();

			if(!Tracker.jobs.Any())
			{
				Console.WriteLine("");
				Console.WriteLine("   There is only emptiness");
				Console.WriteLine("");

			} else {

				Tracker.jobs.ForEach((job) =>
				{
					var duration = job.GetDuration();
					total += duration;
					Console.WriteLine("{0,2} u {1,2} m     {2}",
									  duration.Hours,
									  duration.Minutes,
									  job.Name + (job.IsActive ? " (Still active)" : "")
									 );
				});
			}

			// Footer

			Console.WriteLine(new String((char)0x2550, 70));
			Console.WriteLine("{0,2} u {1,2} m", total.Hours,total.Minutes);

			return 0;
		}
	}
}
