using Domain.Backlogs;
using Domain.Developers;
using Domain.Pipelines;
using Domain.Sprints.SprintStates;
using System;
using System.Collections.Generic;

namespace Domain.Sprints
{
	public abstract class Sprint
	{
		private readonly Project project;
		private readonly List<BacklogItem> backlogItems;

		private string name;
		private DateTime startDate;
		private DateTime endDate;
		private SprintState state;

		private readonly Developer scrumMaster;
		private readonly List<Developer> developers;

		private Pipeline pipeline;


		public Sprint(Project project, string name, DateTime startDate, DateTime endDate, Developer scrumMaster, List<Developer> developers)
		{
			this.project = project;
			this.name = name;
			this.startDate = startDate;
			this.endDate = endDate;
			this.scrumMaster = scrumMaster;
			this.developers = developers;

			backlogItems = new List<BacklogItem>();

			state = new ScheduledState(this);
		}

		public void SetPipeline(Pipeline pipeline)
		{
			this.pipeline = pipeline;
		}

		public Pipeline GetPipeline()
		{
			return pipeline;
		}

		public void AddDeveloper(Developer developer)
		{
			developers.Add(developer);
		}

		public void AddToSprintBacklog(BacklogItem backlogItem)
		{
			//Add backlogItem to sprintbacklog only when it is not already in the sprintbacklog
			if (!backlogItems.Contains(backlogItem))
			{
				backlogItem.SetSprint(this);
			}

			backlogItems.Add(backlogItem);
		}

		public void ChangeState(SprintState state)
		{
			this.state = state;
		}

		public List<BacklogItem> GetBacklogItems()
		{
			return backlogItems;
		}

		public List<Developer> GetDevelopers()
		{
			return developers;
		}

		public DateTime GetEndDate()
		{
			return endDate;
		}

		public string GetName()
		{
			return name;
		}

		public Project GetProject()
		{
			return project;
		}

		public Developer GetScrumMaster()
		{
			return scrumMaster;
		}

		public DateTime GetStartDate()
		{
			return startDate;
		}

		public SprintState GetState()
		{
			return state;
		}

		public void SetEndDate(DateTime endDate)
		{
			this.endDate = pipeline == null || pipeline.GetStatus() != PipelineJobStatusType.Running
				? endDate
				: throw new Exception("Can't change end date when pipeline is running");
		}

		public void SetName(string name)
		{
			this.name = pipeline == null || pipeline.GetStatus() != PipelineJobStatusType.Running
				? name
				: throw new Exception("Can't change end date when pipeline is running");
		}

		public void SetStartDate(DateTime startDate)
		{
			this.startDate = pipeline == null || pipeline.GetStatus() != PipelineJobStatusType.Running
				? startDate
				: throw new Exception("Can't change end date when pipeline is running");
		}





	}
}
