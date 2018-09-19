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
using Timetracker.Output;

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
			
			var table = BuildTable();

			if(Date.SameDay(DateTime.Now))
				table.Caption = "Summary of today";
			else
				table.Caption = string.Format(
					"Summary of {0}-{1}-{2}",
					Date.Day.ToString().PadLeft(2, '0'),
					Date.Month.ToString().PadLeft(2, '0'),
					Date.Year
				);
			

			// Summary table

			Console.WriteLine(string.Empty);

			foreach(var row in table.Generate())
				Console.WriteLine(row);

			Console.WriteLine(string.Empty);

			return 0;
		}


		TableGenerator BuildTable()
		{
			TimeSpan total = new TimeSpan();
			double totalNum = 0d;

			var table = new TableGenerator()
			{
				CellPadding = 2,
				ContainsHeader = true,
				ContainsFooter = Tracker.jobs.Any(),
			};

			table.AddRow("Time", "", "Job");

			if(Tracker.jobs.Any())
			{
				Tracker.jobs.ForEach((job) =>
				{
					var duration = job.GetDuration();
					var durationNum = job.GetDurationAsFloat();

					totalNum += durationNum;
					total += duration;

					table.AddRow(
						FormatTime(duration),
						string.Format("{0:0.00}", durationNum),
						job.Name + (job.IsActive ? " ← Current Job" : string.Empty)
					);
				});

				table.AddRow(FormatTime(total), string.Format("{0:0.00}", totalNum));

			} else {
				table.AddRow("There is only emptiness");
			}

			return table;
		}

		string FormatTime(TimeSpan duration) 
			=> string.Format("{0}:{1,2}", duration.Hours, duration.Minutes.ToString().PadLeft(2, '0'));

	}
}
