using System;
using System.Collections.Generic;

namespace Domain.Forums
{
	public class Forum
	{
		private readonly List<Thread> threads;

		public Forum()
		{
			threads = new List<Thread>();
		}

		public void AddThread(Thread thread)
		{
			if (thread.GetActivity().GetStatus() == Backlogs.ActivityStatus.Done)
			{
				throw new Exception("Can't add a thread to a done activity");
			}

			if (string.IsNullOrWhiteSpace(thread.GetTitle()))
			{
				throw new Exception("Can't add a thread without a title");
			}

			threads.Add(thread);
		}

		public void RemoveThread(Thread thread)
		{

			if (!threads.Contains(thread))
			{
				throw new Exception("Can't remove a thread that doesn't exist");
			}

			_ = threads.Remove(thread);
		}

		public List<Thread> GetThreads()
		{
			return threads;
		}
	}
}
