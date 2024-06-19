using Domain.Backlogs.BacklogItemStates;
using Domain.Backlogs.Enums;
using Domain.Developers;
using Domain.Notifications;
using Domain.Notifications.Interfaces;
using Domain.Sprints;
using System;
using System.Collections.Generic;

namespace Domain.Backlogs
{
	public class BacklogItem : INotificationObservable
	{
		private string name;
		private string description;
		private int effort;

		private readonly List<Activity> activities;
		private Developer assignedDeveloper;

		private readonly Backlog backlog;
		private Sprint sprint;

		private BacklogItemState state;
		private readonly List<INotificationObserver> observers;

		public BacklogItem(string name, string description, int effort, Backlog backlog)
		{
			this.name = name;
			this.description = description;
			this.effort = effort;
			state = new TodoState(this);
			this.backlog = backlog;
			observers = new List<INotificationObserver>();

			activities = new List<Activity>();
		}

		public void SetSprint(Sprint sprint)
		{
			this.sprint = sprint;
		}

		public Sprint GetSprint()
		{
			return sprint;
		}

		public bool AllActivitiesDone()
		{
			foreach (Activity activity in activities)
			{
				if (activity.GetStatus() != ActivityStatus.Done)
				{
					return false;
				}
			}
			return true;
		}

		public bool AllActivitiesDoneOrActive()
		{
			foreach (Activity activity in activities)
			{
				if (activity.GetStatus() != ActivityStatus.Todo)
				{
					return false;
				}
			}
			return true;
		}

		public void SetName(string name)
		{
			this.name = name;
		}

		public string GetName()
		{
			return name;
		}

		public void SetDescription(string description)
		{
			this.description = description;
		}

		public string GetDescription()
		{
			return description;
		}

		public void SetEffort(int effort)
		{
			this.effort = effort;
		}

		public int GetEffort()
		{
			return effort;
		}

		public void AssignDeveloper(Developer newAssignedDeveloper)
		{
			assignedDeveloper = newAssignedDeveloper;
			Register(new Notificator(assignedDeveloper));
		}

		public Developer GetAssignedDeveloper()
		{
			return assignedDeveloper;
		}

		public void ChangeState(BacklogItemState state)
		{
			//The state of the backlogItem can only be changed once it has a sprint reference
			if (sprint == null)
			{
				throw new Exception("The backlogItem is not part a sprint so state can't be changed");
			}
			this.state = state;
			Notify(this.state.GetState().ToString());
		}

		public BacklogStateType GetStateType()
		{
			return state.GetState();
		}

		public BacklogItemState GetState()
		{
			return state;
		}

		public List<Activity> GetActivities()
		{
			return activities;
		}

		public void AddActivity(Activity activity)
		{
			if (!activities.Contains(activity))
			{
				activities.Add(activity);
			}
		}

		public bool RemoveActivity(Activity activity)
		{
			return activities == null
				? throw new NotSupportedException("There are no tasks in this backlogItem")
				: activities.Remove(activity);
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
