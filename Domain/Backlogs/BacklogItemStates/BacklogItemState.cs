using Domain.Backlogs.Enums;

namespace Domain.Backlogs.BacklogItemStates
{
	public abstract class BacklogItemState
	{
		private readonly BacklogItem _backlogItem;

		public BacklogItemState(BacklogItem backlogItem)
		{
			_backlogItem = backlogItem;
		}
		public virtual void AddActivity(Activity activity)
		{
			_backlogItem.AddActivity(activity);
		}

		public virtual void RemoveActivity(Activity activity)
		{
			_ = _backlogItem.RemoveActivity(activity);
		}

		public virtual void SetDescription(string description)
		{
			_backlogItem.SetDescription(description);
		}

		public virtual void SetEffort(int newEffort)
		{
			_backlogItem.SetEffort(newEffort);
		}

		public virtual void SetName(string newName)
		{
			_backlogItem.SetName(newName);
		}

		public BacklogItem GetBacklogItem()
		{
			return _backlogItem;
		}

		public abstract BacklogStateType GetState();

		public abstract void NextState();
		public abstract void PreviousState();
	}
}
