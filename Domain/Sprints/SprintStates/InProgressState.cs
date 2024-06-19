using System;

namespace Domain.Sprints.SprintStates
{
	internal class InProgressState : SprintState
	{
		private readonly Sprint sprint;
		public InProgressState(Sprint sprint) : base(sprint)
		{
			this.sprint = sprint;
		}

		public override void NextState()
		{
			sprint.ChangeState(new FinishedState(sprint));
			sprint.GetState().StartStateAction();
		}

		public override void PreviousState()
		{
			sprint.ChangeState(new ScheduledState(sprint));
		}

		public override void StartStateAction()
		{
			throw new Exception($"No action for Scheduled state");
		}

		public override SprintStateTypes GetSprintState()
		{
			return SprintStateTypes.InProgress;
		}
	}
}
