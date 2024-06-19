using System;

namespace Domain.Sprints.SprintStates
{
	internal class FinishedState : SprintState
	{
		private readonly Sprint sprint;

		public FinishedState(Sprint sprint) : base(sprint)
		{
			this.sprint = sprint;
		}

		public override void SetReview(Review review)
		{
			if (sprint.GetType().Name == "ReviewSprint")
			{
				if (review.Author == sprint.GetScrumMaster())
				{
					((ReviewSprint)sprint).AddReview(review);
				}
				else
				{
					throw new Exception("Only scrummaster can add a review");
				}
			}
			throw new Exception("Review can't be added to release sprint");
		}

		public override void NextState()
		{
			throw new Exception($"No next state for {GetSprintState()} state");
		}

		public override void PreviousState()
		{
			sprint.ChangeState(new InProgressState(sprint));
		}

		public override void StartStateAction()
		{
			switch (sprint.GetType().Name)
			{
				case "ReleaseSprint":
					sprint.GetPipeline().Execute();
					break;
				case "ReviewSprint":
					sprint.GetPipeline()?.Execute();
					break;
			}
		}

		public override SprintStateTypes GetSprintState()
		{
			return SprintStateTypes.Finished;
		}
	}
}
