using System;
using Timetracker.Tracker;
using Timetracker.src.Commands;
using CommandLine;

namespace Timetracker
{
	class Program
	{		

		static void Main(string[] args)
		{
			CommandLine.Parser.Default.ParseArguments<StartJobCommand, EndJobCommand, ListJobsCommand>(args)
					   .MapResult(
				           (StartJobCommand cmd) => cmd.Run(),
				           (EndJobCommand cmd) => cmd.Run(),
			               (ListJobsCommand cmd) => cmd.Run(),
				           _ => 1
				          );
		}
	}
}

