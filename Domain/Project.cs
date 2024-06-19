using Domain.Backlogs;
using Domain.Developers;
using Domain.Developers.Enums;
using Domain.Forums;
using Domain.Pipelines;
using Domain.SCM;
using Domain.Sprints;
using System;
using System.Collections.Generic;

namespace Domain
{
	public class Project
	{
		private readonly string name;

		private readonly Developer productOwner;
		private readonly List<Developer> testers;

		private readonly Backlog backlog;
		private Forum forum;
		private readonly List<Sprint> sprints;
		private readonly Repository repository;
		private readonly List<Pipeline> pipelines;

		public Project(Developer productOwner, string name)
		{
			this.productOwner = productOwner;
			this.name = name;

			testers = new List<Developer>();
			sprints = new List<Sprint>();

			repository = new Repository("master");
			pipelines = new List<Pipeline>();
			backlog = new Backlog(this);
		}

		public void AddSprint(Sprint sprint)
		{
			sprints.Add(sprint);
		}

		public List<Sprint> GetSprints()
		{
			return sprints;
		}

		public Developer GetProductOwner()
		{
			return productOwner;
		}

		public Backlog GetBacklog()
		{
			return backlog;
		}

		public string GetName()
		{
			return name;
		}

		public List<Developer> GetTesters()
		{
			return testers;
		}

		public void AddTester(Developer tester)
		{
			//check if developer is already a tester
			if (tester.GetRole() != RoleType.Tester)
			{
				throw new Exception("Developer is not a tester");
			}

			if (testers.Contains(tester))
			{
				throw new Exception("Tester is already in the project");
			}

			testers.Add(tester);
		}

		public Forum GetForum()
		{
			return forum;
		}

		public void CreateForum()
		{
			if (forum != null)
			{
				throw new Exception("Forum already exists");
			}

			forum = new Forum();
		}

		public void AddPipeline(Pipeline pipeline)
		{
			pipelines.Add(pipeline);
		}

		public List<Pipeline> GetPipelines()
		{
			return pipelines;
		}

		public Repository GetRepository()
		{
			return repository;
		}
	}
}
