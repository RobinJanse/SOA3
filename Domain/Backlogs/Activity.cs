using Domain.Developers;
using System;

namespace Domain.Backlogs
{
	public class Activity
	{
		private string description;
		private Developer assignedDeveloper;
		private ActivityStatus status;

		public Activity(string description)
		{
			this.description = description;
		}

		//Set discription but one when _status is done
		public void SetDescription(string description)
		{
			this.description = status != ActivityStatus.Done ? description : throw new Exception("Activity is done, cannot change description when DONE");
		}

		public string GetDescription()
		{
			return description;
		}

		public Developer GetAssignedDeveloper()
		{
			return assignedDeveloper;
		}

		public void SetAssignedDeveloper(Developer developer)
		{
			assignedDeveloper = status != ActivityStatus.Done ? developer : throw new Exception("Activity is done, cannot change developer when DONE");
		}

		public ActivityStatus GetStatus()
		{
			return status;
		}


		public void NextStatus()
		{
			switch (status)
			{
				case ActivityStatus.Todo:
					status = ActivityStatus.Active;
					break;
				case ActivityStatus.Active:
					status = ActivityStatus.Done;
					break;
				case ActivityStatus.Done:
					throw new Exception("Activity is already done");
			}
		}

		public void PreviousStatus()
		{
			switch (status)
			{
				case ActivityStatus.Todo:
					throw new Exception("Activity is already todo");
				case ActivityStatus.Active:
					status = ActivityStatus.Todo;
					break;
				case ActivityStatus.Done:
					status = ActivityStatus.Active;
					break;
			}
		}
	}

	public enum ActivityStatus
	{
		Todo,
		Active,
		Done
	}
}