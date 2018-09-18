using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Newtonsoft.Json;

namespace Timetracker.Tracker
{
	
	public class Tracker
	{

		public List<Job> jobs { get; private set; } = new List<Job>();

		[JsonIgnoreAttribute]
		public Job ActiveJob { get; private set; }

		public DateTime Date { get; private set; }



		public Tracker(DateTime date)
		{
			Date = date;
		}


		/// <summary>
		/// Ends the current job and starts a new one.
		/// </summary>
		/// <returns>The previous job or null</returns>
		public Job Push(string name)
		{
			var ended = End();
			if(ended != null && ended.Name == name)
				return null;
			
			var job = Find(name);
			if(job == null)
				jobs.Add(job = new Job() { Name = name });

			ActiveJob = job;

			if(job.IsActive)
				return null;

			job.Start();

			return ended;
		}


		/// <summary>
		/// Find a job with the specified name.
		/// </summary>
		public Job Find(string name)
		{
			return jobs.Where(j => j.Name == name).FirstOrDefault();
		}



		/// <summary>
		/// End a current running job
		/// </summary>
		public Job End()
		{
			if(jobs.Count < 1)
				return null;

			var currentJob = jobs.Where(j => j.IsActive).FirstOrDefault();

			currentJob?.End();

			return currentJob;
		}
	}
}

