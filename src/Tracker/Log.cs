using System;

namespace Timetracker.Tracker
{
	public class Log
	{
		/// <summary>
		/// Begin time of the job
		/// </summary>
		public DateTime Begin { get; set; }


		/// <summary>
		/// Endtime of the job. Or null is still running
		/// </summary>
		public DateTime? End { get; set; }
	}

}
