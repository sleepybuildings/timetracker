using System;
using System.IO;
using Newtonsoft.Json;

namespace Timetracker.Tracker
{
	public class Store
	{
		

		/// <summary>
		/// Loads a tracker instance from file. Creates a new tracker if the instance is not found.
		/// </summaxry>
		public Tracker BuildTracker(DateTime dateTime)
		{
			Tracker tracker = null;
			var filename = GetFilename(dateTime);

			if(File.Exists(filename))
				tracker = Deserialize(filename);

			return tracker ?? new Tracker(DateTime.Now);
		}


		/// <summary>
		/// Saves the tracker to a file
		/// </summary>
		public bool StoreTracker(Tracker tracker)
		{
			return Serialize(GetFilename(tracker.Date), tracker);
		}


		private string GetStorageDirectory()
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "timetracker");
			if(!File.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}


		private string GetFilename(DateTime dateTime)
		{
			return Path.Combine(GetStorageDirectory(), dateTime.ToString("yyyy-M-d") + ".json");
		}


		private Tracker Deserialize(string filename)
		{
			try
			{
				using(StreamReader trackerFile = File.OpenText(filename))
				{
					return new JsonSerializer().Deserialize(trackerFile, typeof(Tracker)) as Tracker;
				}

			} catch(Exception ex)
			{
				Console.WriteLine("Unable to deserialize tracker file: " + filename + ": " + ex.Message);
				return null;
			}
		}


		private bool Serialize(string filename, Tracker tracker)
		{
			try
			{
				using(StreamWriter trackerFile = File.CreateText(filename))
				{
					new JsonSerializer().Serialize(trackerFile, tracker);
				}

				return true;

			} catch(Exception ex)
			{
				Console.WriteLine("Unable to serialize tracker file: " + filename + ": " + ex.Message);
				return false;
			}
		}


	}
}
