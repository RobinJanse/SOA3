using Domain.Notifications.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.SCM
{
	public class Branch : INotificationObservable
	{
		private readonly string name;
		private DateTime changeDate;
		private Code code;

		private readonly List<INotificationObserver> observers;

		public Branch(string name)
		{
			this.name = name;
			changeDate = DateTime.Now;
			code = new Code("");
			observers = new List<INotificationObserver>();
		}


		public string GetName()
		{
			return name;
		}

		public DateTime GetChangeDate()
		{
			return changeDate;
		}

		public Code GetCode()
		{
			return code;
		}

		public void PushCommit(Commit commit)
		{
			code = commit.GetCode();
			Notify($"new commit pushed has been pushed to {name}");
			changeDate = DateTime.Now;
		}

		public void Register(INotificationObserver observer)
		{
			observers.Add(observer);
		}

		public void UnRegister(INotificationObserver observer)
		{
			_ = observers.Remove(observer);
		}

		public void Notify(string message)
		{
			foreach (INotificationObserver observer in observers)
			{
				observer.Update(message);
			}
		}
	}
}