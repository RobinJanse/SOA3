using System;
using System.Collections.Generic;

namespace Domain.Backlogs
{
	public class Backlog
	{
		private readonly List<BacklogItem> backlogItems;
		private readonly Project project;

		public Backlog(Project project)
		{
			this.project = project;
			backlogItems = new List<BacklogItem>();
		}

		public void AddBacklogItem(BacklogItem backlogItem)
		{
			if (backlogItems.Contains(backlogItem))
			{
				throw new Exception("Can't add the same backlogItem twice");
			}

			backlogItems.Add(backlogItem);
		}

		public void RemoveBacklogItem(BacklogItem backlogItem)
		{
			if (!backlogItems.Contains(backlogItem))
			{
				throw new Exception("BacklogItem not found");
			}

			_ = backlogItems.Remove(backlogItem);
		}

		public List<BacklogItem> GetBacklogItems()
		{
			return backlogItems;
		}

		public Project GetProject()
		{
			return project;
		}
	}
}
