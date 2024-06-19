using Domain.Backlogs;
using Domain.Developers;
using System;

namespace Domain.Sprints.SprintStates
{
	public class ScheduledState : SprintState
	{
		private readonly Sprint sprint;
		public ScheduledState(Sprint sprint) : base(sprint)
		{
			this.sprint = sprint;
		}

		public override void SetName(string name)
		{
			sprint.SetName(name);
		}

		public override void SetStartDate(DateTime startDate)
		{
			sprint.SetStartDate(startDate);
		}

		public override void SetEndDate(DateTime endDate)
		{
			sprint.SetEndDate(endDate);
		}

		public override void AddDeveloper(Developer developer)
		{
			sprint.AddDeveloper(developer);
		}

		public override void AddToSprintBacklog(BacklogItem backlogItem)
		{
			sprint.AddToSprintBacklog(backlogItem);
		}

		public override void NextState()
		{
			sprint.ChangeState(new InProgressState(sprint));
		}

		public override void PreviousState()
		{
			throw new Exception($"No previous state for Scheduled state");
		}

		public override void StartStateAction()
		{
			throw new Exception($"No action for Scheduled state");
		}

		public override SprintStateTypes GetSprintState()
		{
			return SprintStateTypes.Scheduled;
		}
	}
}
