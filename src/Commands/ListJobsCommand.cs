using System;
using CommandLine;
using System.Collections.Generic;
using Timetracker.Extensions;
using System.Linq;
using System.Drawing;
using Colorful;
using Timetracker.Tracker;
using ColorConsole = Colorful.Console;
using Console = System.Console;

namespace Timetracker.Commands
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
			
			Console.WriteLine(string.Empty);

			// Summary table

			Console.WriteLine(" Timespan            Job");
			Console.WriteLine(new String((char)0x2550, 70));

			TimeSpan total = new TimeSpan();

			if(!Tracker.jobs.Any())
			{
				Console.WriteLine(string.Empty);
				ColorConsole.WriteLine("   There is only emptiness", Color.Silver);
				Console.WriteLine(string.Empty);

			} else {
			 
				total = PrintSummary();
			}

			// Footer

			Console.WriteLine(new String((char)0x2550, 70));

			if(Tracker.jobs.Any())
			{
				Console.WriteLine("{0,2} u {1,2} m", total.Hours, total.Minutes);
				Console.WriteLine(string.Empty);
			}

			return 0;
		}


		TimeSpan PrintSummary()
		{
			TimeSpan total = new TimeSpan();

			var alternator = new ColorAlternatorFactory().GetAlternator(1, Color.Red, Color.Green);

			Tracker.jobs.ForEach((job) =>
			{
				var duration = job.GetDuration();
				total += duration;
				ColorConsole.WriteLineAlternating(FormatTableRow(job, duration), alternator);
			});

			return total;
		}


		string FormatTableRow(Job job, TimeSpan duration)
		{
			return string.Format("{0,2} u {1,2} m    {2:0.00}    {3}",
								  duration.Hours,
								  duration.Minutes,
			                      job.GetDurationAsFloat(),
			                      job.Name + (job.IsActive ? " ← Current Job" : "")
								);
		}
	}
}
