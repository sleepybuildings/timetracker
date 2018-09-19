using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Timetracker.Tracker
{
	public class Job
	{

		/// <summary>
		/// Past and current job starts
		/// </summary>
		public List<Log> logs { get; private set; } = new List<Log>();


		/// <summary>
		/// Name of the job
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Start a new log entry for this job
		/// </summary>
		public void Start()
		{
			logs.Add(new Log()
			{
				Begin = DateTime.Now
			});
		}



		/// <summary>
		/// Counts the duration of this job
		/// </summary>
		public TimeSpan GetDuration()
		{
			TimeSpan result = new TimeSpan();
			logs.ForEach(log => result += (log.End.HasValue? log.End.Value : DateTime.Now) - log.Begin);

			return result;
		}


		public double GetDurationAsFloat()
		{
			return GetDuration().TotalSeconds / 60d;
		}


		public Log OldestEntry => logs.OrderBy(l => l.End ?? DateTime.Now).First();


		/// <summary>
		/// Ends a running log
		/// </summary>
		public void End() => logs.Where(l => l.End == null).ToList().ForEach(i => i.End = DateTime.Now);


		/// <summary>
		/// True when this job is running
		/// </summary>
		public bool IsActive => logs.Where(l => l.End == null).Count() > 0;


		public Log ActiveLog => logs.Where(l => l.End == null).FirstOrDefault();

	}
}

