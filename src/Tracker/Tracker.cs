using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;

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
		/// Stops the current job and resume the previous
		/// </summary>
		public (Job poppedJob, Job startedJob) Pop()
		{
			Job startedJob = null;

			var poppedJob = CurrentJob;
			if(poppedJob != null)
			{
				startedJob = FindPrecedingJob(poppedJob);

				if(startedJob != null)
					Push(startedJob.Name);
			}

			return (poppedJob, startedJob);
		}


		/// <summary>
		/// Find the job which ended before the given job
		/// </summary>
		private Job FindPrecedingJob(Job job)
		{
			var activeLog = job.ActiveLog;
			if(activeLog == null)
				return null;

			var jobStarted = activeLog.Begin;

			return jobs.Where(j => j.Name != job.Name)
					   .Where(j => j.OldestEntry.End <= activeLog.Begin)
				       .OrderByDescending(j => j.OldestEntry.End)
					   .FirstOrDefault();
		}


		private Job CurrentJob => jobs.Where(j => j.IsActive).FirstOrDefault();


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

			var currentJob = CurrentJob;

			currentJob?.End();

			return currentJob;
		}
	}
}

